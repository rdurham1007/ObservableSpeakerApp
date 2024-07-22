using MediatR;
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

        public GetSpeakersQueryHandler(SpeakerDbContext dbContext)
        {
            _dbContext = dbContext;    
        }

        public Task<GetSpeakersQueryResult> Handle(GetSpeakersQuery request, CancellationToken cancellationToken)
        {
            var speakers = _dbContext.Speakers.Select(s => new SpeakerApp.Domain.Speakers.Speaker
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Bio = s.Bio,
                CreatedAt = s.CreatedAt
            }).ToList();

            return Task.FromResult(new GetSpeakersQueryResult { Speakers = speakers });
        }
    }
}