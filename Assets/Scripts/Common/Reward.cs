using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Represents a Reward, possible extensions could be an Item, Achievement...
/// </summary>
public abstract class Reward
{
	public int identifier { get; }
	public string name { get; }
	public string description { get; }

}