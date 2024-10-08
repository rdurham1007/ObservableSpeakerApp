using MassTransit;
using MediatR;
using SpeakerApp.Domain.Speakers.Commands;
using SpeakersService.App;

namespace SpeakersService.Service.Consumers
{
    public class CreateSpeakerConsumer : 
        IConsumer<CreateSpeaker>
    {
        private readonly ILogger<CreateSpeakerConsumer> _logger;
        private readonly IMediator _mediator;

        public CreateSpeakerConsumer(ILogger<CreateSpeakerConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CreateSpeaker> context)
        {
            try
            {
                var response = await _mediator.Send(new CreateSpeakerCommand
                {
                    FirstName = context.Message.FirstName,
                    LastName = context.Message.LastName,
                    Email = context.Message.Email,
                    Bio = context.Message.Bio,
                    CreatedAt = context.Message.CreatedAt
                });

                await context.RespondAsync<CreateSpeakerResult>(response);

                _logger.LogInformation("CreateSpeaker consumed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming CreateSpeaker");
            }
        }
    }
}