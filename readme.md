### Docker commands

- **build docker image**: `docker build -t pigeon-bot-dotnet .`
- **package docker image**: `docker save -o pigeon-bot-dotnet.tar pigeon-bot-dotnet`

### Container config

- map /app/data path
- set Discord__Token and Discord__GuildId variables