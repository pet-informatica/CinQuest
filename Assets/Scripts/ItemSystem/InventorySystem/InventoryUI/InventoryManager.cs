using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	public GameObject inventoryCanvas;

	// Use this for initialization
	void Start () {
		inventoryCanvas.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		CloseInventory ();
	}

	public void CloseInventory() {
		if (Input.GetButtonDown("Inventory")) {
			if (inventoryCanvas.activeSelf) {
				inventoryCanvas.SetActive (false);
			} else {
				inventoryCanvas.SetActive (true);
			}
		}
	}
}
