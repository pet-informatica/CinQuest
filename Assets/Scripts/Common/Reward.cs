using System;

public class Reward
{
	private int identifier { get; }
	private string name { get; }
	private string description { get; }

	public Reward (int identifier, string name, string description)
	{
		this.identifier = identifier;
		this.name = name;
		this.description = description;
	}
}