using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using System.Reflection;

namespace PigeonBotDotnet;

public class Program
{
    public static async Task Main(string[] args)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services
            .AddScoped<ILiteDatabase>(sp => new LiteDatabase("data/pigeon-bot.db"))
            .AddDiscordGateway(options => options.Intents = NetCord.Gateway.GatewayIntents.Guilds | NetCord.Gateway.GatewayIntents.GuildMessages | NetCord.Gateway.GatewayIntents.MessageContent)
            .AddApplicationCommands(options => options.AutoRegisterCommands = false)
            .AddGatewayHandlers(assembly);

        var host = builder.Build();

        host.AddModules(assembly);

        await host.RunAsync();
    }
}