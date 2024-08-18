namespace TalksService.Service.Consumers
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using SpeakerApp.Domain.Talks;
    using TalksService.Data;

    public class GetTalksConsumer : IConsumer<GetTalks>
    {
        private readonly ILogger<GetTalksConsumer> logger;
        private readonly TalksDbContext _repository;

        public GetTalksConsumer(ILogger<GetTalksConsumer> logger, TalksDbContext repository)
        {
            this.logger = logger;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetTalks> context)
        {
            this.logger.LogInformation("GetTalksConsumer: {0}", context.Message);

            IEnumerable<TalkRecord> talks;

            if (context.Message.SpeakerId.HasValue)
            {
                talks = await _repository.Talks
                    .Where(t => t.SpeakerId == context.Message.SpeakerId.Value)
                    .ToListAsync();
            }
            else
            {
                talks = await _repository.Talks.ToListAsync();
            }

            var result = new GetTalksResult
            {
                Talks = talks.Select(t => new Talk
                {
                    Id = t.Id,
                    Title = t.Title,
                    Abstract = t.Abstract,
                    Speaker = t.SpeakerId
                }).ToList()
            };

            await context.RespondAsync(result);
        }
    }
}