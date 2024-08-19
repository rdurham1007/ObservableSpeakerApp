# Tracing Demo

These notes are for exploring tracing within the .NET services. They're a bit sparse at the moment but should be enough to give you some ideas.

## Viewing Traces

All traces are captured using the OpenTelemetry .NET libraries and exported to an OTEL collector running as part of the infrastructure setup by [compose.infra.yml](../compose.infra.yml) that was used in the setup.

To view these traces you can use the Jaeger app: http://localhost:16686/search

Any action you perform in the web app should have a corresponding trace, so you're encouraged to explore a little and then check out Jaeger to view the different traces you generated.

## Basic Trace

- View Speakers on the main page
  - https://localhost:5173/
- View the trace in Jaeger
  - http://localhost:16686/search
  - Service: "SpeakerApp.BFF"
  - Lookback: "Last 5 minutes"
  - "Find Traces"
- Explain trace id propagation
  - View logs in "SpeakerService"
  - See "traceparent" header

## Attributes && Baggage

- Got to RMQ and add an observer queue
  - http://localhost:15672/
    - admin
    - password
  - [Queues and Streams](http://localhost:15672/#/queues)
    - Create queue "CreateTalk_Observer"
    - Attach Queue to "CreateTalk" exchange
  - Create a Talk
    - View message in CreateTalk_Observer
      - Look at headers for trace id and baggage

## All Services Trace

- Create a talk with a new speaker
- Observe the trace

## Customer Activity (Trace)

- Use SpeakerService
- Use the TraceBehavior
  - Activity source
  - Start activity