using System.Collections.Generic;

namespace SpeakerApp.Domain.Speakers.Queries
{
    public record GetSpeakers
    {
    }

    public record GetSpeakersResult
    {
        public List<Speaker> Speakers { get; init; } = new();
    }
}