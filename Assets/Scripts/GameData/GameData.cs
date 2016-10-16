using System;

[Serializable]
public class GameData
{
	public string PlayerName { get; set; }

	public GameData ()
	{
		PlayerName = "";
	}
}
