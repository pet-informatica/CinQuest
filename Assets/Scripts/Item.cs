using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public ItemType itemType;
	
	public enum ItemType{
		Weapon,
		Consumable,
		Quest
	}

	/*
	 *Create an Item with specifics atributes
	 */
	public Item(string itemName, int itemID, string itemDesc, ItemType itemType){
		this.itemName = itemName;
		this.itemID = itemID;
		this.itemDesc = itemDesc;
		this.itemIcon = Resources.Load<Texture2D> ("Icons/" + this.itemName);
		this.itemType = itemType;
	}

	/*
	 *Create empty item 
	 */
	public Item(){
		this.itemID = -1;
	}
}
