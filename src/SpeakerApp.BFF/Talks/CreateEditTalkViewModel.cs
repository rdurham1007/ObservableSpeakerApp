using SpeakerApp.BFF.Speakers;

namespace SpeakerApp.BFF.Talks
{
    public record CreateEditTalkViewModel
    {
        public Guid? Id { get; init; }
        public string Title { get; init; }
        public string Abstract { get; init; }

        public Guid? SpeakerId => Speaker?.Id;

        public string SpeakerName => $"{Speaker.FirstName} {Speaker.LastName}";

        public CreateEditSpeakerModel? Speaker { get; init; }
    }
}