using System.Security.Cryptography;
using NetCord.Services.ApplicationCommands;

namespace PigeonBotDotnet.Modules;

public class PigeonModule : ApplicationCommandModule<ApplicationCommandContext>
{

	[SlashCommand("fact", "Get a random pigeon fact.")]
	public string GetPigeonFact()
	{
		var facts = File.ReadAllLines("data/pigeon-facts.txt");
		var index = RandomNumberGenerator.GetInt32(facts.Length);

		return facts[index];
	}

}