namespace SpeakerApp.Domain.Speakers
{
    public record Speaker
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual string FirstName { get; set; } = string.Empty;
        public virtual string LastName { get; set; } = string.Empty;
        public virtual string Email { get; set; } = string.Empty;
        public virtual string Bio { get; set; } = string.Empty;
        public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}