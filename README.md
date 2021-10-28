# NatsChat

A console and API chat client for NATS

## Getting started

Download the source code either by downloading it directly from GitHub as a zip or clone the project by running the following in your terminal of choice:
```text
git clone git@github.com:AndBlo/NatsChat.git
```

### Build and run the projects

#### Prerequisites
You need to have a NATS server running, follow the instructions [here](https://docs.nats.io/nats-server/installation) to get started

#### Console client
##### Commandline
Publish an executable by running the following from the solution folder (use -o parameter to specify output path):
```text
dotnet publish NatsChat.ConsoleApp -c Release
```

Run the app by providing the following arguments:
- *[username]* - Your selected username in the chat room.
- *[subject]* - The subject to subscribe and publish messages to.
- *[url]* - The optional url for the NATS server. Defaults to NATS default url (nats://localhost:4222)

```text
.\NatsChat.ConsoleApp [username] [subject] [url?]
```

To run the console client directly from the project files, navigate to the solution folder and run the following
```text
dotnet run --project NatsChat.ConsoleApp [username] [subject] [url?]
```

#### Docker
Build the docker image from the projects Dockerfile by running the following command from the solution folder:
```text
docker build -f NatsChat.ConsoleApi/Dockerfile -t myimagename .
```

Start a container from your newly built docker image by running the following:
```text
docker run -it --network=host myimagename [username] [subject] [url?]
```

#### Web API client
##### Commandline
Publish an executable of the API by running the following from the solution folder (use -o parameter to specify output path):
```text
dotnet publish NatsChat.WebApi -c Release
```

Run the app by providing the following arguments:
- *[subject]* - The subject to subscribe and publish messages to.
- *[url]* - The optional url for the NATS server. Defaults to NATS default url (nats://localhost:4222)

```text
.\NatsChat.WebApi ChatSettings:Subject=[subject] ChatSettings:NatsUrl=[url?]
```

To run the web api client directly from the project files, navigate to the solution folder and run the following
```text
dotnet run --project NatsChat.WebApi ChatSettings:Subject=[subject] ChatSettings:NatsUrl=[url?]
```

#### Docker
Build the docker image from the projects Dockerfile by running the following command from the solution folder:
```text
docker build -f NatsChat.WebApi/Dockerfile -t myimage .
```

Start a container from your newly built docker image by running the following:
```text
docker run -p [myPort]:80 -e "ChatSettings:Subject=[subject]" -e "ChatSettings:NatsUrl=[url]" --name mycontainer myimage
```

#### Consume the API
To publish a new message to the chat room, make the following call in your http client of choice:
```text
POST: https://{host}:{port}/api/chat/messages
BODY: { "id": "apiUser", "messageText": "Hello, world!" }
```
To get all received messages since the client started, curl the following:
```text
GET: https://{host}:{port}/api/chat/messages
```