using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Item Manager
/// </summary>
public class ItemManager {

    private IGenericItemRepository _itemRepository;

    public ItemManager(IGenericItemRepository _itemRepository)
    {
        this._itemRepository = _itemRepository;
    }

    /// <summary>
    /// Gets the itens.
    /// </summary>
    /// <returns>The itens.</returns>
    public Dictionary<int, GenericItem> getItems()
    {
        if (this._itemRepository.items.Count < 0)
        {
            throw new Exception("No items were found!");
        }
        return this._itemRepository.items;
    }

    /// <summary>
    /// Returns an item in the repository by identifier
    /// </summary>
    /// <param name="identifier">The item identifier</param>
    /// <returns>The item object, or null if not found</returns>
    public GenericItem getItem(int identifier)
    {
        return this._itemRepository.items[identifier];
    }

    /// <summary>
    /// Loads the itens from repository.
    /// </summary>
    /// <returns><c>true</c>, if quests from repository was loaded, <c>false</c> otherwise.</returns>
    public bool loadItemsFromAssets()
    {
        this.tryLoadItems();

        if (this._itemRepository.items.Count > 0)
            return true;

        return false;
    }

    /// <summary>
    /// Tries the load items.
    /// </summary>
    private void tryLoadItems()
    {
        this._itemRepository.loadItens();
    }
}
