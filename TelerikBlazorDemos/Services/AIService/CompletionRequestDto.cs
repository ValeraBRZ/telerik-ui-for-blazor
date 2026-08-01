using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelerikBlazorDemos.Services
{
    public class CompletionRequestDto
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";

        [JsonPropertyName("contents")]
        public List<ChatContentRequestDto> Contents { get; set; } = new();
    }

    public class ChatContentRequestDto
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; } = "text";

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}
