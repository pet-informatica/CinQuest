using UnityEngine;
using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Represents a GenericItem, possible extensions could be an Item, NotShowableItem, Achievement...
/// </summary>
[CreateAssetMenu(menuName = "ItemsSystem/GenericItem")]
[Serializable]
public class GenericItem : ScriptableObject
{
	[SerializeField]
	private int identifier = GameManager.Instance.idGenerator.GenerateID ();
	public int Identifier 
	{
		get { return identifier; }
	}

	[SerializeField]
	private string name;
	public string Name
	{
		get { return name; }
		set { this.name = name; }
	}

}