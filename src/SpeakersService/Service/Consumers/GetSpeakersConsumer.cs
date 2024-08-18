using MassTransit;
using MediatR;
using SpeakerApp.Domain.Speakers.Queries;
using SpeakersService.App;

namespace SpeakersService.Service.Consumers
{
    public class GetSpeakersConsumer : 
        IConsumer<GetSpeakers>
    {
        private readonly ILogger<GetSpeakersConsumer> _logger;
        private readonly IMediator _mediator;

        public GetSpeakersConsumer(ILogger<GetSpeakersConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<GetSpeakers> context)
        {
            try
            {
                var response = await _mediator.Send(new GetSpeakersQuery());

                await context.RespondAsync<GetSpeakersResult>(response);

                _logger.LogInformation("GetSpeakers consumed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming GetSpeakers");
            }

        }
    }
}