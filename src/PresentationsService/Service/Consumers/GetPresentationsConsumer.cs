using MassTransit;
using Microsoft.EntityFrameworkCore;
using PresentationsService.Data;
using SpeakerApp.Domain.Presentations;

namespace PresentationsService.Service.Consumers
{
    public class GetPresentationsConsumer : IConsumer<GetPresentations>
    {
        private readonly ILogger<GetPresentationsConsumer> _logger;
        private readonly PresentationsDbContext _dbContext;

        public GetPresentationsConsumer(ILogger<GetPresentationsConsumer> logger, PresentationsDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<GetPresentations> context)
        {
            var request = context.Message;

            var query = _dbContext.Presentations.AsQueryable();

            if (request.SpeakerId.HasValue)
            {
                query = query.Where(p => p.SpeakerId == request.SpeakerId);
            }

            var presentations = await query.ToListAsync();

            var result = new GetPresentationsResult
            {
                Presentations = presentations.Select(p => new Presentation
                {
                    Id = p.Id,
                    Location = p.Location,
                    ScheduledAt = p.ScheduledAt,
                    TalkId = p.TalkId,
                    Title = p.Title,
                    Abstract = p.Abstract,
                    SpeakerId = p.SpeakerId,
                    SpeakerName = p.SpeakerName,
                    SpeakerBio = p.SpeakerBio,
                    CreatedAt = p.CreatedAt
                }).ToList()
            };

            await context.RespondAsync(result);
        }
    }
}