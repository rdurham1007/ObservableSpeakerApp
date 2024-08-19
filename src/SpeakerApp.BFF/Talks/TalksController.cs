namespace SpeakerApp.BFF.Talks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using SpeakerApp.Domain.Speakers.Commands;
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
        public async Task<ActionResult<CreateTalkResult>> Post(CreateEditTalkViewModel talk)
        {            

            if(talk.Speaker.Id == null)
            {
                var requestClient = _serviceBus.CreateRequestClient<CreateSpeaker>();
                var speakerResponse = await requestClient.GetResponse<CreateSpeakerResult>(new CreateSpeaker
                {
                    FirstName = talk.Speaker.FirstName,
                    LastName = talk.Speaker.LastName,
                    Email = talk.Speaker.Email,
                    Bio = talk.Speaker.Bio,
                    CreatedAt = DateTime.UtcNow
                });

                talk.Speaker.Id = speakerResponse.Message.Id;
            }

            var activity = Activity.Current;

            
            if(activity != null)
            {
                activity.AddTag("talk.title", talk.Title);
                activity.AddTag("talk.abstract", talk.Abstract);
                activity.AddTag("talk.speakerId", talk.SpeakerId.ToString());

                activity.AddBaggage("clientId", "12345");

            }

            var talkClient = _serviceBus.CreateRequestClient<CreateTalk>();
            var response = await talkClient.GetResponse<CreateTalkResult>(talk);
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