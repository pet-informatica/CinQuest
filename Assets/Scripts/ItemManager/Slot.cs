using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a slot in the inventory. Can have a item on it, or be empty.
/// </summary>
public class Slot : MonoBehaviour, IPointerEnterHandler {

	public Sprite empty;
	public Item Item {	get; private set;	}
	public Text itemName;
	public Text itemDescription;


	Image image;

	void Awake(){
		image = GetComponent<Image> ();
	}

	/// <summary>
	/// Places an item in the slot and updates the slot description when hovered
	/// </summary>
	/// <param name="item">The item to place</param>
	public void SetItem(Item item){
		if (item != null) {
			Item = item;
			image.sprite = item.sprite;
		} else {
			item = null;
			image.sprite = empty;
		}
	}

	/// <summary>
	/// Shows item description and title when slot is hovered
	/// </summary>
	/// <param name="data">Pointer event data.</param>
	public void OnPointerEnter(PointerEventData data){
		if (Item != null) {
			itemName.text = Item.title;
			itemDescription.text = Item.description;
		}
	}
		
}
