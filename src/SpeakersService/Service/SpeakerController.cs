using Microsoft.AspNetCore.Mvc;
using MassTransit;
using SpeakerApp.Domain.Speakers.Commands;
using SpeakerApp.Domain.Speakers.Queries;
using MediatR;
using SpeakersService.App;

namespace SpeakersService.Service
{
    [ApiController]
    [Route("api/speakers")]
    public class SpeakerController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly ILogger<SpeakerController> _logger;
        private readonly IMediator _mediator;

        public SpeakerController(IBus bus, ILogger<SpeakerController> logger, IMediator mediator)
        {
            _bus = bus;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeaker(CreateSpeaker command)
        {
            var client = _bus.CreateRequestClient<CreateSpeaker>();            
            var response = await client.GetResponse<CreateSpeakerResult>(command);
            return Ok(response.Message);
        }

        public async Task<IActionResult> GetSpeakers()
        {
            foreach (var header in Request.Headers)
            {
                _logger.LogInformation("HEADER: {Key}: {Value}", header.Key, header.Value);
            }

            var result = await _mediator.Send(new GetSpeakersQuery() {});
            
            return Ok(new GetSpeakersResult { Speakers = result?.Speakers ?? new List<SpeakerApp.Domain.Speakers.Speaker>() });
        }
    }
}