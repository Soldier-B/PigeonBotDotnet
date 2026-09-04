# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
# Publish as self-contained and enable trimming
RUN dotnet publish -c Release -o /app/publish \
    --self-contained true \
    -p:PublishTrimmed=true

# Final Stage: Bare dependencies image (no full .NET runtime needed)
FROM mcr.microsoft.com/dotnet/runtime-deps:10.0-noble-chiseled-extra AS final
WORKDIR /app

COPY --from=build /app/publish .

USER root
ENTRYPOINT ["./PigeonBotDotnet"]