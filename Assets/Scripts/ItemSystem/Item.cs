using UnityEngine;
using System;
using System.Collections;

[CreateAssetMenu(menuName = "ItemsSystem/Item")]
[Serializable]
public class Item : GenericItem 
{
	public Sprite icon;
	public string description;
	public bool disposable;
}
