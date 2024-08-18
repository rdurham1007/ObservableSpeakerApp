namespace TalksService.Data
{
    public class TalkRecord
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public Guid SpeakerId { get; set; }
        public string SpeakerName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}