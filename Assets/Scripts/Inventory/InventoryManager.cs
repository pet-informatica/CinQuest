using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class InventoryManager : MonoBehaviour {

	public GameObject inventoryCanvas;
	private MenuStatus menuStatus;

	// Use this for initialization
	void Start () {
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
