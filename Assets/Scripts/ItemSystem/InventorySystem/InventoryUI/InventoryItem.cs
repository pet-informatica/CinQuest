using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemSelected;
	public static bool noParent;
	Transform originalParent;
	Vector3 originalSlot;
	public string name = "Elmo do amigo Petiano.";
	public string description = "Um elmo estiloso da colecao de chapeus de Peao.\n\r Use-o com sabedoria.";

	void Start () {

	}

	public void OnBeginDrag (PointerEventData eventData) {
		itemSelected = gameObject;
		originalSlot = gameObject.transform.position;
		originalParent = gameObject.transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		noParent = true;
	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
		transform.SetParent(originalParent.transform.parent);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		itemSelected = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (noParent) {
			transform.position = originalSlot;
			transform.SetParent(originalParent);
		}
	}

	void Update () {
		
	}
}
