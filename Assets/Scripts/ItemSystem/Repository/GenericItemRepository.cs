using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Item Repository
/// </summary>
public class GenericItemRepository : IGenericItemRepository
{
    private Dictionary<int, GenericItem> items;

    public Dictionary<int, GenericItem> Items {
        get { return items; }
    }

    /// <summary>
    /// Adds an item in the repository
    /// </summary>
    /// <param name="item">The item for adding</param>
    /// <returns>Returns false if the repository alredy contains an item with that identifier. True otherwise.</returns>
    public bool AddItem(GenericItem item)
    {
        if (items.ContainsKey(item.Identifier))
            return false;
        items.Add(item.Identifier, item);
        return true;
    }

    public bool LoadItens()
    {
        //UNDER CONSTRUCTION
        items = new Dictionary<int, GenericItem>();
		if (items.Count == 0)
			Console.Write ("No items\n");

		UnityEngine.Object[] data;
		data = AssetDatabase.LoadAllAssetsAtPath("Assets/Prefabs/Items");

		foreach (GenericItem i in data) {
			items.Add (i.Identifier, i);
			Console.Write (i.Name + "\n");
		}

		if (items.Count > 0) {
			Console.Write ("Deu certo!\n");
			return true;
		} 

		return false;
    }

    /// <summary>
    /// Removes an item from the repository
    /// </summary>
    /// <param name="identifier">The identifier of the item to remove</param>
    /// <returns>Returns false if the repository doens't have an item with that identifier. True otherwise.</returns>
    public bool RemoveItem(int identifier)
    {
        return items.Remove(identifier);
    }

    /// <summary>
    /// Search for an item in the repository
    /// </summary>
    /// <param name="identifier">The identifier of the item for searching</param>
    /// <returns>Returns the Item found, or null if there is no item with that identifier.</returns>
    public GenericItem SearchItem(int identifier)
    {
        return items[identifier];
    }

    /// <summary>
    /// Updates the value of an item in the repository
    /// </summary>
    /// <param name="identifier">The item identifier</param>
    /// <param name="item">The new item object updated</param>
    /// <returns>Returns false if the is no item with that identifier to update, or true if it was sucessfully updated</returns>
    public bool UpdateItem(int identifier, GenericItem item)
    {
        GenericItem retrieveItem = this.SearchItem(identifier);
        if (retrieveItem == null)
            return false;
        retrieveItem = item;
        return true;
    }
}
