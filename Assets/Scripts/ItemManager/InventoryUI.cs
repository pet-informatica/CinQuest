using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour {

	public GameObject inventory;
	public Slot[] slots;
	bool opened;
	MenuStatus menuStatus;

	void Start () {
		menuStatus = GameManager.Instance.menuStatus;
		CloseWindow ();
	}

	void UpdateContent(){
		foreach (Slot slot in slots)
			slot.SetItem (null);
		
		for(int i = 0, j = 0; j < User.Instance.Items.Count; ++j) {
			if (User.Instance.Items [j] is Item) {
				slots [i++].SetItem(((Item)User.Instance.Items [j]));
			}
		}
	}

	/// <summary>
	/// Open the Inventory Canvas, setting it's gameobject to active
	/// </summary>
	public void OpenWindow()
	{
		
		inventory.SetActive(true);
		UpdateContent ();
		opened = true;
		menuStatus.open ("Inventory");
	}

	/// <summary>
	/// Closes the Inventory Canvas, setting it's gameobject to active
	/// </summary>
	public void CloseWindow()
	{
		opened = false;
		menuStatus.close ("Inventory");
		inventory.SetActive (false);
	}

	void Update ()
	{
		
		if (Input.GetButtonDown("Inventory") && !menuStatus.openProblem("Inventory"))
		{
			if (opened) CloseWindow();
			else OpenWindow();
		}
	}
}
