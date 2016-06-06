using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Item Repository
/// </summary>
public class GenericItemRepository : IGenericItemRepository
{
    private Dictionary<int, GenericItem> _items;

    public Dictionary<int, GenericItem> items {
        get {
            return _items;
        }
    }

    /// <summary>
    /// Adds an item in the repository
    /// </summary>
    /// <param name="item">The item for adding</param>
    /// <returns>Returns false if the repository alredy contains an item with that identifier. True otherwise.</returns>
    public bool addItem(GenericItem item)
    {
        if (items.ContainsKey(item.identifier))
            return false;
        items.Add(item.identifier, item);
        return true;
    }

    public bool loadItens()
    {
        //TODO: LOAD ITENS FROM PROJECT ASSETS VIA CODE
        return false;
    }

    /// <summary>
    /// Removes an item from the repository
    /// </summary>
    /// <param name="identifier">The identifier of the item to remove</param>
    /// <returns>Returns false if the repository doens't have an item with that identifier. True otherwise.</returns>
    public bool removeItem(int identifier)
    {
        return items.Remove(identifier);
    }

    /// <summary>
    /// Search for an item in the repository
    /// </summary>
    /// <param name="identifier">The identifier of the item for searching</param>
    /// <returns>Returns the Item found, or null if there is no item with that identifier.</returns>
    public GenericItem searchItem(int identifier)
    {
        return items[identifier];
    }

    /// <summary>
    /// Updates the value of an item in the repository
    /// </summary>
    /// <param name="identifier">The item identifier</param>
    /// <param name="item">The new item object updated</param>
    /// <returns>Returns false if the is no item with that identifier to update, or true if it was sucessfully updated</returns>
    public bool updateItem(int identifier, GenericItem item)
    {
        GenericItem retrieveItem = this.searchItem(identifier);
        if (retrieveItem == null)
            return false;
        retrieveItem = item;
        return true;
    }
}
