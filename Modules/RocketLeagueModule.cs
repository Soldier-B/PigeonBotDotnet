using LiteDB;
using NetCord.Services.ApplicationCommands;
using PigeonBotDotnet.Models;
using System.Security.Cryptography;

namespace PigeonBotDotnet.Modules;

public class RocketLeageModule : ApplicationCommandModule<ApplicationCommandContext>
{
	private readonly ILiteDatabase _db;

	public RocketLeageModule(ILiteDatabase db)
	{
		_db = db;
	}

	[SlashCommand("lineup", "Pick a random Boomin' Birds lineup.")]
	public async Task<string> GetRandomLineup()
	{
		List<Teammate> roster = [.. GetTeam().FindAll()];

		while (roster.Count > 3)
			roster.RemoveAt(RandomNumberGenerator.GetInt32(roster.Count));

		List<string> lineup = [.. await Task.WhenAll(
			roster.Select(async tm => await GetDisplayName(tm.Snowflake))
		)];

		lineup.Sort(StringComparer.OrdinalIgnoreCase);

		return $"The roster is {lineup[0]}, {lineup[1]} and {lineup[2]}.";
	}

	[SlashCommand("leader", "Who's turn is it to lead the party?")]
	public async Task<string> GetNextLeader()
	{
		var leader = GetTeam().FindOne(tm => tm.IsLeader && tm.IsLeading);
		var name = await GetDisplayName(leader.Snowflake);

		return $"The next leader is **{name}**.";
	}

	[SlashCommand("leading", "Tell me you're leading the party.")]
	public async Task<string> SetNextLeader()
	{
		var team = GetTeam();
		List<Teammate> leaders = [.. team
			.Find(tm => tm.IsLeader)
			.OrderByDescending(tm => tm.IsLeading)
		];

		if (leaders[0].Snowflake == Context.User.Id)
		{
			var name = await GetDisplayName(leaders[1].Snowflake);

			foreach (var leader in leaders)
				leader.IsLeading = !leader.IsLeading;

			team.Update(leaders);

			return $"**{name}** has been set as the next leader.";
		}

		return "You're not the current leader.";
	}

	private ILiteCollection<Teammate> GetTeam()
	{
		return _db.GetCollection<Teammate>("Team");
	}

	private async Task<string> GetDisplayName(ulong snowflake)
	{
		var member = await Context.Client.Rest.GetGuildUserAsync(Context.Guild!.Id, snowflake);
		return member.Nickname ?? member.GlobalName ?? member.Username;
	}

}