namespace SpeakerApp.Domain.Presentations
{
    public class Presentation
    {
        public Guid Id { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime ScheduledAt { get; set; }
        public Guid TalkId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public Guid SpeakerId { get; set; }
        public string SpeakerName { get; set; } = string.Empty;
        public string SpeakerBio { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}