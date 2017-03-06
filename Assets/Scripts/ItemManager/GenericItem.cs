using UnityEngine;

/// <summary>
/// Developed by: Peao (rngs);
/// Represents a GenericItem, possible extensions could be an Item, NotShowableItem, Achievement...
/// </summary>
public class GenericItem : ScriptableObject
{
	public int identifier;

	public GenericItem(int identifier){
		this.identifier = identifier;
	}
}