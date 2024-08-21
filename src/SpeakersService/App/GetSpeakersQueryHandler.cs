using System.Diagnostics;
using System.Diagnostics.Metrics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SpeakerApp.Domain.Speakers.Queries;
using SpeakersService.Data;

namespace SpeakersService.App
{
    public record GetSpeakersQuery : IRequest<GetSpeakersQueryResult>
    {

    }

    public record GetSpeakersQueryResult : GetSpeakersResult
    {

    }

    public class GetSpeakersQueryHandler : IRequestHandler<GetSpeakersQuery, GetSpeakersQueryResult>
    {
        private readonly SpeakerDbContext _dbContext;
        private readonly SpeakerServiceMetrics _metrics;

        public GetSpeakersQueryHandler(SpeakerDbContext dbContext, SpeakerServiceMetrics metrics)
        {
            _dbContext = dbContext;
            _metrics = metrics;
        }

        public async Task<GetSpeakersQueryResult> Handle(GetSpeakersQuery request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();            

            var speakers = await _dbContext.Speakers.Select(s => new SpeakerApp.Domain.Speakers.Speaker
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Bio = s.Bio,
                CreatedAt = s.CreatedAt
            }).ToListAsync();

            sw.Stop();

            _metrics.RecordGetSpeakersDbQueryDuration(sw.Elapsed.TotalMilliseconds);

            Activity.Current?.SetTag("SpeakersQueryDurationMS", sw.Elapsed.TotalMilliseconds);

            return new GetSpeakersQueryResult { Speakers = speakers };
        }
    }
}