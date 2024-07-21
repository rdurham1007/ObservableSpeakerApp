namespace SpeakerApp.Domain.Common
{
    public record RequestResult<T>
    {
        public RequestResult(T message)
        {
            Message = message;
        }
        public bool Success { get; init; } = true;
        public required T Message { get; init; }

        public string ErrorMessage { get; init; } = string.Empty;
    }
}