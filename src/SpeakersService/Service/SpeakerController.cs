using Microsoft.AspNetCore.Mvc;
using MassTransit;
using SpeakerApp.Domain.Speakers.Commands;

namespace SpeakersService.Service
{
    [ApiController]
    [Route("api/speakers")]
    public class SpeakerController : ControllerBase
    {
        private readonly IBus _bus;

        public SpeakerController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeaker(CreateSpeaker command)
        {
            var client = _bus.CreateRequestClient<CreateSpeaker>();            
            var response = await client.GetResponse<CreateSpeakerResult>(command);
            return Ok(response.Message);
        }
    }
}