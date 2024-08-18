namespace SpeakersService.Service.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using SpeakerApp.Domain.Speakers;
    using SpeakersService.Data;

    public class DeleteSpeakerConsumer : IConsumer<DeleteSpeaker>
    {
        private readonly ILogger<DeleteSpeakerConsumer> _logger;
        private readonly SpeakerDbContext _speakersDbContext;

        public DeleteSpeakerConsumer(ILogger<DeleteSpeakerConsumer> logger, SpeakerDbContext speakersDbContext)
        {
            _logger = logger;
            _speakersDbContext = speakersDbContext;
        }

        public async Task Consume(ConsumeContext<DeleteSpeaker> context)
        {
            _logger.LogInformation("DeleteSpeakerConsumer: {Id}", context.Message.Id);

            var speakerId = context.Message.Id;
            var speaker = await _speakersDbContext.Speakers.FindAsync(speakerId);

            if (speaker != null)
            {
                _speakersDbContext.Speakers.Remove(speaker);
                await _speakersDbContext.SaveChangesAsync();
                _logger.LogInformation("Speaker with ID {Id} has been deleted.", speakerId);
            }
            else
            {
                _logger.LogInformation("Speaker with ID {Id} does not exist.", speakerId);
            }

            await context.RespondAsync<DeleteSpeakerResult>(new());

        }
    }
}