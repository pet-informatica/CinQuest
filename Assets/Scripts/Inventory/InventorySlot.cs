using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

	void Start () {

	}

	void Update () {
	}

	public void OnDrop (PointerEventData eventData)
	{
		if (transform.childCount == 0) {
			InventoryItem.itemSelected.transform.SetParent (transform);
			InventoryItem.itemSelected.transform.position = transform.position;
			InventoryItem.noParent = false;
		}
	}
}
