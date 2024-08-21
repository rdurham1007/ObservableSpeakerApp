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
        private readonly IConfiguration _configuration;

        public SpeakerController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<SpeakerViewModel>> GetSpeakers()
        {

            var endpoint = _configuration.GetValue<string>("SpeakerServiceEndpoint") ?? "http://kubernetes.docker.internal:5034";

            var client = new HttpClient() { BaseAddress = new Uri(endpoint) };
            
            var result = await client.GetFromJsonAsync<GetSpeakersResult>("api/speakers");

            var speakers = result?.Speakers.Select(s => new SpeakerViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Bio = s.Bio,
                CreatedAt = s.CreatedAt
            });

            return speakers ?? Enumerable.Empty<SpeakerViewModel>();
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