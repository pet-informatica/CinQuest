using UnityEngine;
using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Represents a GenericItem, possible extensions could be an Item, NotShowableItem, Achievement...
/// </summary>
[CreateAssetMenu]
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

	public GenericItem() {}
	/*
	public void OnEnable()
	{
		identifier = GameManager.Instance.idGenerator.GenerateID ();
		this.name = name;
	}*/
}