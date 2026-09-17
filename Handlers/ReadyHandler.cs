using Microsoft.Extensions.Configuration;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Services.ApplicationCommands;

public class ReadyHandler : IReadyGatewayHandler
{
	private readonly GatewayClient _client;
	private readonly ApplicationCommandService<ApplicationCommandContext> _service;
	private readonly IConfiguration _configuration;

	public ReadyHandler(GatewayClient client, ApplicationCommandService<ApplicationCommandContext> service, IConfiguration configuration)
	{
		_client = client;
		_service = service;
		_configuration = configuration;
	}

	public async ValueTask HandleAsync(ReadyEventArgs args)
	{
		var guildId = _configuration.GetValue<ulong>("Discord:GuildId");

		await _service.RegisterCommandsAsync(_client.Rest, _client.Id, guildId);
	}
}