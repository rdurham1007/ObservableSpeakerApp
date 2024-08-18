namespace SpeakerApp.Domain.Talks
{
    public record Talk
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Abstract { get; init; } = string.Empty;
        public Guid Speaker { get; init; }
    }
}