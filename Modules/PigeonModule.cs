using System.Security.Cryptography;
using NetCord.Services.ApplicationCommands;
using PigeonBotDotnet.Models;
using System.Text.Json;

namespace PigeonBotDotnet.Modules;

public class PigeonModule : ApplicationCommandModule<ApplicationCommandContext>
{

	[SlashCommand("fact", "Get a random pigeon fact.")]
	public async Task<string> GetPigeonFact()
	{
		var facts = await File.ReadAllLinesAsync("data/facts.txt");
		var index = RandomNumberGenerator.GetInt32(facts.Length);

		return facts[index];
	}

	[SlashCommand("vibe", "Get a vibe check.")]
	public async Task<string> GetVibe()
	{
		int vibeLevel = RandomNumberGenerator.GetInt32(101);
		var vibe = await FindVibe(vibeLevel);

		if (vibe is null) return "...vibe not found...";

		return $"**{vibeLevel}% - {vibe.Title}** {vibe.Message}";
	}

	private async Task<Vibe?> FindVibe(int vibe)
	{
		using FileStream fs = File.OpenRead("data/vibes.json");
		var vibes = await JsonSerializer.DeserializeAsync<List<Vibe>>(fs, JsonSerializerOptions.Web);

		return vibes?.Find(v => v.Limit >= vibe);
	}

}