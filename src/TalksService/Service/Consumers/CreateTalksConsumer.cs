namespace TalksService.Service.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using SpeakerApp.Domain.Talks;
    using TalksService.Data;

    public class CreateTalksConsumer : IConsumer<CreateTalk>
    {
        private readonly ILogger<CreateTalksConsumer> _logger;
        private readonly TalksDbContext _talksDbContext;

        public CreateTalksConsumer(ILogger<CreateTalksConsumer> logger, TalksDbContext talksDbContext)
        {
            _logger = logger;
            _talksDbContext = talksDbContext;
        }

        public async Task Consume(ConsumeContext<CreateTalk> context)
        {
            var command = context.Message;

            var talkRecord = new TalkRecord
            {
                Id = command.Id,
                Title = command.Title,
                Abstract = command.Abstract,
                SpeakerId = command.SpeakerId,
                SpeakerName = command.SpeakerName,
                CreatedAt = command.Created
            };

            _talksDbContext.Talks.Add(talkRecord);
            await _talksDbContext.SaveChangesAsync();

            _logger.LogInformation("Talk created: {TalkId}", talkRecord.Id);

            await context.RespondAsync<CreateTalkResult>(new { Talk = talkRecord });
        }
    }
}