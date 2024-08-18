using MassTransit;
using SpeakerApp.Domain.Talks;
using TalksService.Data;

namespace TalksService.Service.Consumers
{
    public class RetireTalkConsumer : IConsumer<RetireTalk>
    {
        private readonly ILogger<RetireTalkConsumer> _logger;
        private readonly TalksDbContext _dbContext;

        public RetireTalkConsumer(ILogger<RetireTalkConsumer> logger, TalksDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<RetireTalk> context)
        {
            var talk = await _dbContext.Talks.FindAsync(context.Message.Id);

            if (talk is null)
            {
                _logger.LogWarning("Talk with id {Id} not found", context.Message.Id);
                return;
            }

            _dbContext.Talks.Remove(talk);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Talk with id {Id} deleted", context.Message.Id);

            await context.RespondAsync<RetireTalkResult>(new ());
        }
    }
}