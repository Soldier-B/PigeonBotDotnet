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

	[SlashCommand("vibe", "Get a vibe check.")]
	public string GetVibe()
	{
		int vibe = RandomNumberGenerator.GetInt32(101);
		var message = ("THE PIGEON.", "No human, only pigeon.");

		switch (vibe)
		{
			case <= 10:
				message = ("Absolutely Not.", "The flock has rejected your application.");
				break;
			case <= 20:
				message = ("Needs Supervision.", "Please return to the nest.");
				break;
			case <= 30:
				message = ("Suspicious Human.", "The pigeons are watching you.");
				break;
			case <= 40:
				message = ("Acceptable.", "You may remain on the sidewalk.");
				break;
			case <= 50:
				message = ("Breadworthy.", "You have demonstrated sufficient bread acquisition potential.");
				break;
			case <= 60:
				message = ("Pretty Pigeon.", "Honestly? You've got something going on.");
				break;
			case <= 70:
				message = ("One of the Guys.", "You may join the flock, but don't make it weird.");
				break;
			case <= 80:
				message = ("Flock Material.", "The transition is already underway.");
				break;
			case <= 90:
				message = ("Basically Pigeon.", "You have begun to forget what shoes are for.");
				break;
			case <= 99:
				message = ("Pigeon Ascendant.", "Your human form is becoming increasingly difficult to maintain.");
				break;
		}

		return $"**{vibe}% - {message.Item1}** {message.Item2}";
	}

}