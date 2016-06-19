using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Developed by: Lucas (lss5)
/// The generator of identifier numbers for itens.
/// Created to avoid problems about same id for different items.
/// It consists basically in a "persistent variable" that is serialized 
/// and save its last value.
/// 
/// IMPORTANT: There must be only one IdGenerator in the project, but, since it
/// must not exist only when the game runs, there was no need to create it as a singleton.
/// So, although there's an option at the create menu to create a IdGenerator, it should
/// not be used.
/// </summary>
[CreateAssetMenu(menuName = "ItemsSystem/IDGenerator")]
[Serializable]
public class IdGenerator : ScriptableObject
{
	[SerializeField]
	private int currentID = 0;
	public int CurrentID 
	{
		get{ return currentID; }
	}

	public int GenerateID()
	{
		currentID++;
		return currentID;
	}

}
