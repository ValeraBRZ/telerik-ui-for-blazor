using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelerikBlazorDemos.Services
{
    public class CompletionResponseDto
    {
        [JsonPropertyName("messages")]
        public List<CompletionResponseMessageDto> Messages { get; set; }
    }

    public class CompletionResponseMessageDto
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";

        [JsonPropertyName("contents")]
        public List<CompletionMessageContentDto> Contents { get; set; } = new();
    }

    public class CompletionMessageContentDto
    {
        [JsonPropertyName("$type")]
        public string Type { get; set; } = "text";

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }
}
