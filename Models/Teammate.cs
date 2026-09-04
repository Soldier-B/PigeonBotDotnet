namespace PigeonBotDotnet.Models;
public class Teammate
{
	public int Id { get; set; }
	public required ulong Snowflake { get; set; }
	public required string Nickname { get; set; }
	public bool IsOwner { get; set; }
	public bool IsLeader { get; set; }
	public bool IsLeading { get; set; }
}