using System;

/// <summary>
/// Information on the player name
/// </summary>
[Serializable]
public class GameData
{
	public string PlayerName { get; set; }

	public GameData ()
	{
		PlayerName = "";
	}
}
