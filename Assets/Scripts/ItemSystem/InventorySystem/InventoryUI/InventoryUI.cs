using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour {

	public GameObject slotPrefab, itemPrefab;
	public Vector2 inventorySize = new Vector2(5, 5);
	public float slotSize;
	public Vector2 windowSize;

	// Use this for initialization
	void Start () {
		for (int i = 1; i <= inventorySize.x; i++) {
			for (int j = 1; j <= inventorySize.y; j++) {
				GameObject slot = Instantiate (slotPrefab) as GameObject;
				slot.transform.parent = this.transform;
				slot.transform.localScale = new Vector3(1, 1, 1);
				slot.name = "Slot_" + i + "_" + j;
				slot.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (windowSize.x / (inventorySize.x + 1) * i, windowSize.y / (inventorySize.y + 1) * -j, 0);
				Vector2 anchor = slot.GetComponent<RectTransform> ().anchoredPosition;
				slot.transform.localPosition = new Vector3(anchor.x + (i-1)*slotSize, anchor.y + (j-1)*slotSize, 0);
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
