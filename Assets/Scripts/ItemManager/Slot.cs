using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler {

	public Sprite empty;
	public Item Item {	get; private set;	}
	public Text itemName;
	public Text itemDescription;


	Image image;

	void Awake(){
		image = GetComponent<Image> ();
	}

	public void SetItem(Item item){
		if (item != null) {
			Item = item;
			image.sprite = item.sprite;
		} else {
			item = null;
			image.sprite = empty;
		}
	}

	public void OnPointerEnter(PointerEventData data){
		if (Item != null) {
			itemName.text = Item.title;
			itemDescription.text = Item.description;
		}
	}
		
}
