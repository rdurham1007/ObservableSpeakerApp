# Observability App Scenarios

These are scenarios within the app you can work through to get used to have to use a combination of traces and metrics to explorer bottlenecks and troubleshoot.

## Using k6

To generate decent metrics for this demo we'll be using the load testing tool k6.

https://grafana.com/docs/k6/latest/set-up/install-k6/#windows

```bash
winget install k6 --source winget
```

## /api/speakers bottleneck

- run `k6 run perf-tests\basic-speakers.js`
- open jaeger
  - last 5 minutes
  - min duration 100ms
  - observe the start time on the db span compared to the run time of the mediatr query/command
  - troubleshoot query
    - Add metric (histogram of sw elapsed time)
      - `histogram_quantile(0.95, rate(GetSpeakersDbQueryDuration_milliseconds_bucket[1m]))`
    - Add tag to activity (sw elapsed time)

## /api/talks bottleneck

- run `k6 run perf-tests\basic-talks.js`
- open jaeger
  - last 5 minutes
  - min duration 3000ms
  - observe the start time on send span compared to the others
  - metrics
    - `histogram_quantile(0.95, rate(messaging_masstransit_delivery_duration_milliseconds_bucket[1m]))`
      - This query provides the 95th percentile of message delivery (sent->received), showing the time within which 95% of messages are delivered.
    - `histogram_quantile(0.95, rate(messaging_masstransit_consume_duration_milliseconds_bucket[1m]))`
      - This query provides the 95th percentile of message consumed, showing the time within which 95% of messages are consumed.
  - why is send taking so long?
    - Google search "MassTransit send is slow" turns up https://github.com/MassTransit/MassTransit/issues/3673
      - RMQ has a batch send option
      - MassTransit disables it by default
  - Enable batching for RMQ (in the common project)