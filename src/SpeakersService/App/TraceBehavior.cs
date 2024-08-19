using System.Diagnostics;
using MediatR;

namespace SpeakersService.App
{
    public class TraceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Create an Activity and start it
            using (var activity = SpeakerServiceActivitySource.Instance.GetActivitySource().StartActivity(typeof(TRequest).Name))
            {
                // Call the next behavior or handler in the pipeline
                var response = await next();

                // Return the response
                return response;
            }
        }
    }
}