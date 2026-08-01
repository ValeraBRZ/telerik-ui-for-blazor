using Azure;
using Microsoft.Extensions.AI;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Telerik.SvgIcons;
using TelerikBlazorDemos.Models;

namespace TelerikBlazorDemos.Services
{
    public class AIPromptService : IChatClient
    {
        #region Properties

        private const string APIKey = "API-Key";
        private const string AIServiceURL = "https://demos.telerik.com/service/v2/ai/completion";
        private const string ServiceUnavailableMessage = "The AI service is currently unavailable. Please try again later.";
        private const string SpecialCharactersMessage = "The AI service returned a response containing special characters that are not yet supported. Try generating another output.";

        private string AIChatServiceURL { get; set; } = "https://demos.telerik.com/service/v2/ai/chat";

        private string AIConversationsServiceURL { get; set; } = "https://demos.telerik.com/service/v2/ai/chat/conversations";

        public ChatClientMetadata Metadata => throw new NotImplementedException();

        #endregion Properties

        #region Methods

        public async Task<string> AnswerQuestion(string prompt, CancellationToken cancellationToken = default)
        {
            prompt ??= string.Empty;

            var payload = new List<CompletionRequestDto>
            {
                new()
                {
                    Role = "user",
                    Contents =
                    [
                        new ChatContentRequestDto
                        {
                            Type = "text",
                            Text = prompt.Replace("\"", string.Empty)
                        }
                    ]
                }
            };

            var serializedMessage = JsonConvert.SerializeObject(payload, Formatting.Indented);
            string completion = string.Empty;
            using var client = new HttpClient();

            HttpContent content = new StringContent(serializedMessage, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(AIServiceURL, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                completion = ServiceUnavailableMessage;
                return completion;
            }

            try
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var deserializedResponse = JsonConvert.DeserializeObject<CompletionResponseDto>(responseContent);
                completion = deserializedResponse.Messages.LastOrDefault()?.Contents.LastOrDefault()?.Text ?? string.Empty;
            }
            catch (Exception)
            {
                completion = SpecialCharactersMessage;
            }

            return completion;
        }

        public async Task<ChatResponse> GetResponseAsync(IEnumerable<ChatMessage> chatMessages, ChatOptions options = null, CancellationToken cancellationToken = default)
        {
            var lastChatMessage = chatMessages?.LastOrDefault();
            var lastMessage = lastChatMessage?.Contents?.FirstOrDefault()?.ToString() ?? string.Empty;

            var answer = await AnswerQuestion(lastMessage, cancellationToken);

            var aiMessage = new ChatMessage(ChatRole.Assistant, answer);

            return new ChatResponse(aiMessage);
        }

        public async Task<ChatBotResponse> SendChatBotMessageAsync(List<CompletionRequestDto> messages, CancellationToken cancellationToken = default)
        {
            var serializedMessage = JsonConvert.SerializeObject(messages, Formatting.Indented);

            using var client = new HttpClient();
            HttpContent content = new StringContent(serializedMessage, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(AIServiceURL, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new ChatBotResponse
                {
                    Success = false,
                    Message = ServiceUnavailableMessage,
                    SuggestedActions = new List<string>()
                };
            }

            try
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var deserializedResponse = JsonConvert.DeserializeObject<CompletionResponseDto>(responseContent);
                var botMessageText = deserializedResponse.Messages.LastOrDefault()?.Contents.LastOrDefault()?.Text ?? string.Empty;

                var (regularText, suggestedActions) = ParseBotResponse(botMessageText);

                var botMessage = new CompletionRequestDto
                {
                    Role = "assistant",
                    Contents = new List<ChatContentRequestDto>
                    {
                        new ChatContentRequestDto
                        {
                            Type = "text",
                            Text = botMessageText
                        }
                    }
                };

                return new ChatBotResponse
                {
                    Success = true,
                    Message = regularText,
                    SuggestedActions = suggestedActions,
                    BotMessage = botMessage
                };
            }
            catch (Exception)
            {
                return new ChatBotResponse
                {
                    Success = false,
                    Message = SpecialCharactersMessage,
                    SuggestedActions = new List<string>()
                };
            }
        }

        private (string regularText, List<string> suggestedActions) ParseBotResponse(string botResponse)
        {
            var arrayStart = botResponse.IndexOf('[');
            if (arrayStart == -1)
            {
                return (botResponse, new List<string>());
            }

            var regularText = botResponse.Substring(0, arrayStart).Trim();
            var arrayText = botResponse.Substring(arrayStart).Trim();

            var suggestedActions = new List<string>();
            try
            {
                var optionsArray = JsonConvert.DeserializeObject<List<SuggestedActionDto>>(arrayText);
                suggestedActions = optionsArray?.Select(option => option.Text).ToList() ?? new List<string>();
            }
            catch (Exception)
            {
                // If parsing fails, return empty suggestions
            }

            return (regularText, suggestedActions);
        }

        public async IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponseAsync(IEnumerable<ChatMessage> chatMessages, ChatOptions options = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var lastChatMessage = chatMessages?.LastOrDefault();
            var lastMessage = lastChatMessage?.Contents?.FirstOrDefault()?.ToString() ?? string.Empty;

            string cleanPrompt = lastMessage.Replace("\"", string.Empty);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", APIKey);

            // 1. Create a new chat conversation (send null content)
            var createChatResponse = await client.PostAsync(
                AIConversationsServiceURL,
                null,
                cancellationToken);
            createChatResponse.EnsureSuccessStatusCode();
            var createChatContent = await createChatResponse.Content.ReadAsStringAsync(cancellationToken);
            using var createChatDoc = JsonDocument.Parse(createChatContent);
            var chatId = createChatDoc.RootElement.GetProperty("chatId").GetString();

            // 2. Pass the prompt to the chat service
            var formParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("message", cleanPrompt)
            };
            var formContent = new FormUrlEncodedContent(formParams);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{AIChatServiceURL}/{chatId}?stream=true")
            {
                Content = formContent
            };

            var sendMessageResponse = await client.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);
            using var stream = await sendMessageResponse.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            string line;

            while ((line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false)) != null && !cancellationToken.IsCancellationRequested)
            {
                if (string.IsNullOrWhiteSpace(line) || line == "[DONE]")
                    break;
                if (line.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                    line = line.Substring(5).Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string message = null;
                try
                {
                    using var doc = JsonDocument.Parse(line);
                    var root = doc.RootElement;
                    if (root.TryGetProperty("status", out var statusElem) && statusElem.GetString() == "streaming")
                    {
                        message = root.TryGetProperty("message", out var msgElem) ? msgElem.GetString() : null;
                    }
                }
                catch
                {
                    // Ignore non-JSON lines
                }

                if (!string.IsNullOrEmpty(message))
                {
                    yield return new ChatResponseUpdate(ChatRole.Assistant, message);
                    // Yield control to allow UI updates between chunks
                    await Task.Yield();
                }
            }
        }

        public object GetService(Type serviceType, object serviceKey = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion Methods
    }

    #region Helper Classes

    public class ChatBotResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public List<string> SuggestedActions { get; set; } = new List<string>();

        public CompletionRequestDto BotMessage { get; set; }
    }

    public class SuggestedActionDto
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    #endregion Helper Classes
}
