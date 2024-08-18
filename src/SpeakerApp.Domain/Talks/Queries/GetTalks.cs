using System.Collections.Generic;

namespace SpeakerApp.Domain.Talks
{
    public record GetTalks
    {
        public Guid? SpeakerId { get; init; }
    }

    public record GetTalksResult
    {
        public List<Talk> Talks { get; init; } = new();
    }
}