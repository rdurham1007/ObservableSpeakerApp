namespace SpeakerApp.Domain.Speakers
{
    public record DeleteSpeaker
    {
        public Guid Id { get; init; }
    }

    public record DeleteSpeakerResult
    {
    }
}