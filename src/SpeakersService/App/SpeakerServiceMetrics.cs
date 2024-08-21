using System.Diagnostics.Metrics;

namespace SpeakersService
{
    public class SpeakerServiceMetrics
    {
        private readonly Histogram<double> _getSpeakersDbQueryDuration;

        public SpeakerServiceMetrics(IMeterFactory meterFactory)
        {
            var meter = meterFactory.Create(SpeakerServiceConstants.MeterName);
            _getSpeakersDbQueryDuration = meter.CreateHistogram<double>(
                "GetSpeakersDbQueryDuration",
                "ms",
                "Duration of GetSpeakers query to the database");
        }

        public void RecordGetSpeakersDbQueryDuration(double duration)
        {
            _getSpeakersDbQueryDuration.Record(duration);
        }
        
        public void RecordQueryDuration(double duration, string queryName)
        {
            _getSpeakersDbQueryDuration.Record(duration, new KeyValuePair<string, object?>("QueryName", queryName));
        }
    }
}