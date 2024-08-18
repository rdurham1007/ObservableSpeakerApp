namespace SpeakerApp.BFF.Talks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using SpeakerApp.Domain.Talks;

    [Route("api/[controller]")]
    [ApiController]
    public class TalksController : ControllerBase
    {
        private readonly IBus _serviceBus;

        public TalksController(IBus serviceBus)
        {
            _serviceBus = serviceBus;
        }

        [HttpGet]
        public async Task<ActionResult<GetTalksResult>> Get([FromQuery] Guid? speakerId)
        {
            var requestClient = _serviceBus.CreateRequestClient<GetTalks>();
            var response = await requestClient.GetResponse<GetTalksResult>(new GetTalks() { SpeakerId = speakerId });
            return Ok(response.Message.Talks);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTalkResult>> Post(CreateTalk command)
        {
            var requestClient = _serviceBus.CreateRequestClient<CreateTalk>();
            var response = await requestClient.GetResponse<CreateTalkResult>(command);
            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RetireTalkResult>> Delete(Guid id)
        {
            var requestClient = _serviceBus.CreateRequestClient<RetireTalk>();
            var response = await requestClient.GetResponse<RetireTalkResult>(new RetireTalk { Id = id });
            return Ok(response.Message);
        }
    }
}