using MediatR;
using SpeakerApp.Domain.Speakers.Commands;
using SpeakersService.Data;

namespace SpeakersService.App
{
    internal record CreateSpeakerCommand : CreateSpeaker, IRequest<CreateSpeakerCommandResponse>
    {
        
    }

    internal record CreateSpeakerCommandResponse : CreateSpeakerResult
    {

    }

    internal class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand, CreateSpeakerCommandResponse>
    {
        private readonly SpeakerDbContext _dbContext;

        public CreateSpeakerCommandHandler(SpeakerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<CreateSpeakerCommandResponse> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
        {   
            var speaker = new SpeakerRecord
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Bio = request.Bio,
                CreatedAt = request.CreatedAt,
                Id = Guid.NewGuid()
            };

            _dbContext.Speakers.Add(speaker);

            return Task.FromResult(new CreateSpeakerCommandResponse
            {
                FirstName = speaker.FirstName,
                LastName = speaker.LastName,
                Email = speaker.Email,
                Bio = speaker.Bio,
                CreatedAt = speaker.CreatedAt,
                Id = speaker.Id
            });
        }
    }
}