using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public GameObject inventoryCanvas;
	private MenuStatus menuStatus;
	public GameObject playerName;

	// Use this for initialization
	void Start () {
		playerName.GetComponent<Text>().text = GameManager.Instance.gameData.PlayerName;
		inventoryCanvas.SetActive (false);
		menuStatus = GameManager.Instance.menuStatus;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Inventory") && !menuStatus.openProblem("Inventory")) {
			Debug.Log ("Inventory");
			if (inventoryCanvas.activeSelf) {
				menuStatus.close ("Inventory");
				inventoryCanvas.SetActive (false);
			} else {
				menuStatus.open ("Inventory");
				inventoryCanvas.SetActive (true);
			}
		}
	}
}
