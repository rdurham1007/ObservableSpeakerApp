namespace SpeakerApp.Domain.Presentations
{
    public record GetPresentations
    {
        public Guid? SpeakerId { get; init; }
    }

    public record GetPresentationsResult
    {
        public List<Presentation> Presentations { get; init; } = new();
    }
}