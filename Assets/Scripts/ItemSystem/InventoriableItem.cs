using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(menuName = "ItemsSystem/Item")]
[Serializable]
public class InventoriableItem : GenericItem 
{
	public Sprite icon;
	public string description;
	public bool disposable;
}
