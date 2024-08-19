# Observable Speaker App

This is a sample project to demonstrate various aspects of Observability within .NET applications such as traces in metrics.

## Requirements

To run this app you'll need the folowing:

- Docker (https://www.docker.com/products/docker-desktop/)
- Node (Latest LTS from: https://nodejs.org/en)
- .NET 8 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Quick Start

The quickest way to get started is just let docker spin everything up.

```bash
npm run setup
npm run web
```

With the app running you can explore some of the demos:

[Tracing Demos](./documentation/tracing.md)

## Running the Demo

The demo is split into three pieces; infrastructure, the .net api services (apps) and the web app (react front end).

### Infrastructure

The infrastructure which is just supporting services such as SQL, rabbitmq, and an OpenTelemetry collector. You'll only need to set this up once and you shouldn't have to mess with it again.

If you've already run the quick start, you can skip these step.

`docker-compose -f compose.infra.yml -d`

Once the SQL container is running in docker, run the following to apply migrations and preload the tables with data.

`npm run migrations`

Anytime you want to reset the data, just run that command again to get back to the initial state.

### Apps

These are the .net services used in the demo. To keep you from having to run all three in debug mode, they have been containerized and can be run in docker. This allows you to stop a particular service and run it in debug mode from your IDE while still having the rest of the demo be functional.

To deploy the containers, run the following (if you're run the quick start then this is done for you)

```bash
dotnet build
docker-compose -f compose.apps.yml -d --build
```

This will build each service and use the docker file to create a new image for it before deploying it. If you make changes to a service and want to rebuild the image then you can use the following:

```bash
dotnet build
docker-compose -f compose.apps.yml service-name -d --build
```

Where service name is one of the following:

- speaker-service
- talks-service
- speaker-bff

### Web Applciation

The web application was added just to make it a bit easier to test out different parts of the demo without having to navigate to differnt swagger pages, making various API calls.

To run the app, use the following:

```bash
npm install --prefix ./src/SpeakerApp.Web
npm run web
```