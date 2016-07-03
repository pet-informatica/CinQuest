using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// Inventory model.
/// Developed by: Lucas (lss5)
/// </summary>
public class InventoryModel : MonoBehaviour {
	public List<Item> items { get; set; }
	private List<Token> tokens { get; set; }

	/// <summary>
	/// Determines whether the GenericItem is a Token or not.
	/// </summary>
	/// <returns><c>true</c> if this GenericItem is a Token; otherwise, <c>false</c>.</returns>
	/// <param name="item">Item.</param>
	bool IsToken (GenericItem item) {
		if(item.GetType() == typeof(Token)) return true;
		return false;
	}

	bool AddItem (int identifier) { //At the moment, it's duplicating existing items.
		GameManager gameManager = GameManager.Instance;
		GenericItem temp = gameManager.GetComponent<ItemManager>().GetItem(identifier);
		if (IsToken (temp)) {
			tokens.Add((Token)temp);
			return true;
		} else {
			items.Add((Item)temp);
			return true;
		}
		return false;
	}

	bool RemoveItem (int identifier) {
		GameManager gameManager = GameManager.Instance;
		GenericItem temp = gameManager.GetComponent<ItemManager>().GetItem(identifier);
		if (IsToken (temp)) {
			return tokens.Remove((Token)temp);
		} else {
			return items.Remove((Item)temp);
		}
	}
}
