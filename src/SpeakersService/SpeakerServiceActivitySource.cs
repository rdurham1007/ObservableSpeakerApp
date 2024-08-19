using System.Diagnostics;

public class SpeakerServiceActivitySource
{
    private static readonly object lockObject = new object();
    private static SpeakerServiceActivitySource instance;

    private ActivitySource activitySource;

    private SpeakerServiceActivitySource()
    {
        // Initialize the ActivitySource here
        activitySource = new ActivitySource("SpeakerService");
    }

    public static SpeakerServiceActivitySource Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SpeakerServiceActivitySource();
                    }
                }
            }
            return instance;
        }
    }

    public ActivitySource GetActivitySource()
    {
        return activitySource;
    }
}