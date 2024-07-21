using SpeakerApp.Domain.Common;

namespace SpeakerApp.Domain.Speakers.Commands
{
    public record CreateSpeaker
    {
        public virtual string FirstName { get; init; } = string.Empty;
        public virtual string LastName { get; init; } = string.Empty;
        public virtual string Email { get; init; } = string.Empty;
        public virtual string Bio { get; init; } = string.Empty;
        public virtual DateTime CreatedAt { get; init; }
    }

    public record CreateSpeakerResult : CreateSpeaker
    {
        public virtual Guid Id { get; init; }
    }
}