namespace SpeakerApp.BFF.Talks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using SpeakerApp.Domain.Presentations;

    [Route("api/[controller]")]
    [ApiController]
    public class PresentationsController : ControllerBase
    {
        private readonly IBus _serviceBus;

        public PresentationsController(IBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presentation>>> Get([FromQuery] Guid? speakerId)
        {
            var requestClient = _serviceBus.CreateRequestClient<GetPresentations>();
            var response = await requestClient.GetResponse<GetPresentationsResult>(new GetPresentations() { SpeakerId = speakerId });
            return Ok(response.Message.Presentations);
        }
    }
}