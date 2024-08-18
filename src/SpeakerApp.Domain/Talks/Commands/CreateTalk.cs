namespace SpeakerApp.Domain.Talks
{
    public record CreateTalk
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Abstract { get; init; } = string.Empty;
        public Guid SpeakerId { get; init; }
        public string SpeakerName { get; init; } = string.Empty;

        public DateTime Created { get; init; } = DateTime.UtcNow;
    }

    public record CreateTalkResult
    {
        public Talk Talk { get; init; } = new();
    }
}