using Microsoft.Extensions.Configuration;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Services.ApplicationCommands;

public class ReadyHandler(
	GatewayClient client,
	ApplicationCommandService<ApplicationCommandContext> service,
	IConfiguration configuration) : IReadyGatewayHandler
{

	public async ValueTask HandleAsync(ReadyEventArgs args)
	{
		var guildId = configuration.GetValue<ulong>("Discord:GuildId");

		await service.RegisterCommandsAsync(client.Rest, client.Id, guildId);
	}
}