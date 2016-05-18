using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Represents a GenericItem, possible extensions could be an Item, NotShowableItem, Achievement...
/// </summary>
public class GenericItem
{
	public int identifier { get; set;}
	public string name { get; set;}
	public string description { get; set;}

	public GenericItem(int identifier, string name, string description){
		this.identifier = identifier;
		this.name = name;
		this.description = description;
	}
}