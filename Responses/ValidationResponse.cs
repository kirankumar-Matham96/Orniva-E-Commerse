namespace OrnivaApi.Responses
{
    public class ValidationResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public Dictionary<string, string[]> Errors { get; set; } = [];
    }
}
