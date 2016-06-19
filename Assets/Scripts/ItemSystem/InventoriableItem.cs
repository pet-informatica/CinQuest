using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(menuName = "ItemsSystem/Item")]
[Serializable]
public class InventoriableItem : GenericItem 
{
	public Texture2D icon;
	public string description;
	public bool disposable;
}
