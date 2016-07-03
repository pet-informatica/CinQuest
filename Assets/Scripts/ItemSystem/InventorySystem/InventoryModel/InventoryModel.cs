using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// Inventory model.
/// Developed by: Lucas (lss5)
/// </summary>
public class InventoryModel : MonoBehaviour {
	public Dictionary<int, Item> items { get; set; }
	private Dictionary<int, Token> tokens { get; set; }

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
			tokens.Add(temp.Identifier, (Token)temp);
			return true;
		} else {
			items.Add(temp.Identifier, (Item)temp);
			return true;
		}
		return false;
	}

	bool RemoveItem (int identifier) {
		GameManager gameManager = GameManager.Instance;
		GenericItem temp = gameManager.GetComponent<ItemManager>().GetItem(identifier);
		if (temp == null)
			return false;
		if (IsToken (temp)) {
			return tokens.Remove(identifier);
		} else {
			return items.Remove(identifier);
		}
	}

	bool SearchAtItems (int identifier) {
		return items.ContainsKey (identifier);
	}

	bool SearchArTokens (int identifier) {
		return tokens.ContainsKey (identifier);
	}
	/// <summary>
	/// Checks if the user has the GenericItem.
	/// </summary>
	/// <returns><c>true</c>, if item was checked, <c>false</c> otherwise.</returns>
	/// <param name="identifier">Identifier.</param>
	bool CheckItem (int identifier) {
		return (SearchAtItems (identifier)) || (SearchArTokens (identifier));
	}
}
