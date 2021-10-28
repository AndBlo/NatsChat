#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NatsChat.ConsoleApp/NatsChat.ConsoleApp.csproj", "NatsChat.ConsoleApp/"]
COPY ["NatsChat.Core/NatsChat.Core.csproj", "NatsChat.Core/"]
RUN dotnet restore "NatsChat.ConsoleApp/NatsChat.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/NatsChat.ConsoleApp"
RUN dotnet build "NatsChat.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NatsChat.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NatsChat.ConsoleApp.dll"]