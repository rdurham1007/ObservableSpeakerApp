using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SpeakerApp.Domain.Speakers.Commands;
using SpeakerApp.Domain.Speakers.Queries;

namespace SpeakerApp.BFF.Speakers
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

        [HttpGet]
        public async Task<IEnumerable<SpeakerViewModel>> GetSpeakers()
        {
            var client = _bus.CreateRequestClient<GetSpeakers>();

            var response = await client.GetResponse<GetSpeakersResult>(new { });

            return response.Message.Speakers.Select(s => new SpeakerViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Bio = s.Bio,
                CreatedAt = s.CreatedAt
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpeaker(CreateEditSpeakerModel model)
        {
            var client = _bus.CreateRequestClient<CreateSpeaker>();

            var response = await client.GetResponse<CreateSpeakerResult>(new
            {
                model.FirstName,
                model.LastName,
                model.Email,
                model.Bio,
                model.CreatedAt
            });

            return Ok(response.Message);
        }
    }
}