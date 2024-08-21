# Tracing Demo

These notes are for exploring metrics within the .NET services. They're a bit sparse at the moment but should be enough to give you some ideas.

## Viewing Metrics

For this demo we're sending metrics to an OpenTelemetry collector and Prometheus is scracping those metrics every 15 seconds.

You can view metrics here: http://localhost:9090/

## Using k6

To generate decent metrics for this demo we'll be using the load testing tool k6.

https://grafana.com/docs/k6/latest/set-up/install-k6/#windows

```bash
winget install k6 --source winget
```

## Introduction

- View SpeakersService's [Program.cs](../src/SpeakersService/Program.cs)
  - Notice we add metrics in the same way we add traces
    - Add listeners for specific meters
    - Setup an exporter
- View k6 [basic-speakers.js](../perf-tests/basic-speakers.js) script
  - This is the script we'll be using to load test the application and generate metrics
  - This is just making an API call to the speakers service to fetch the list of speakers

## HTTP Metrics

- Run `k6 run perf-tests\basic-speakers.js`
- Open prometheus: http://localhost:9090/
- Queries to try:
  - `histogram_quantile(.95, rate(http_server_request_duration_seconds_bucket[5m])) * 1000`
    - Calculates the 95th percentile response time over a 5 minute window, meaning 95% of requests are completed in this time or less, expressed in milliseconds.
  - `rate(http_server_request_duration_seconds_sum[3m])/rate(http_server_request_duration_seconds_count[3m]) * 1000`
    - The average request time over a 3 minute window
  - `rate(http_server_request_duration_seconds_count[3m])`
    - The number of requests per second over a 3 minute window

## MassTransit Metrics

- Run `k6 run perf-tests\basic-speakers.js`
- Open prometheus: http://localhost:9090/
- Queries:
  - `histogram_quantile(0.95, rate(messaging_masstransit_delivery_duration_milliseconds_bucket[1m]))`
    - This query provides the 95th percentile of message delivery (sent->received), showing the time within which 95% of messages are delivered.
  - `histogram_quantile(0.95, rate(messaging_masstransit_consume_duration_milliseconds_bucket[1m]))`
    - This query provides the 95th percentile of message consumed, showing the time within which 95% of messages are consumed.
