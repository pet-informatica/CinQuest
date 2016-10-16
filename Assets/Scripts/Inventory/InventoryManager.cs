using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public GameObject inventoryCanvas;
	public GameObject playerName;

	// Use this for initialization
	void Start () {
		playerName.GetComponent<Text>().text = GameManager.Instance.gameData.PlayerName;
		inventoryCanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Inventory")) {
			if (inventoryCanvas.activeSelf) {
				inventoryCanvas.SetActive (false);
			} else {
				inventoryCanvas.SetActive (true);
			}
		}
	}
}
