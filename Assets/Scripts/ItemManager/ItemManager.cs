using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Item Manager
/// </summary>
public class ItemManager {

    private IGenericItemRepository itemRepository;

    public ItemManager(IGenericItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    /// <summary>
    /// Gets the itens.
    /// </summary>
    /// <returns>The itens.</returns>
    public Dictionary<int, GenericItem> GetItems()
    {
        if (this.itemRepository.Items.Count < 0)
        {
            throw new Exception("No items were found!");
        }
        return this.itemRepository.Items;
    }

    /// <summary>
    /// Returns an item in the repository by identifier
    /// </summary>
    /// <param name="identifier">The item identifier</param>
    /// <returns>The item object, or null if not found</returns>
    public GenericItem GetItem(int identifier)
    {
        if (!itemRepository.Items.ContainsKey(identifier))
            return null;
        return itemRepository.Items[identifier];
    }

    /// <summary>
    /// Adds a new item into the repository
    /// </summary>
    /// <param name="item">The item for adding.</param>
    public void AddItem(GenericItem item)
    {
        itemRepository.AddItem(item);
    }

    /// <summary>
    /// Loads the itens from repository.
    /// </summary>
    /// <returns><c>true</c>, if quests from repository was loaded, <c>false</c> otherwise.</returns>
    public bool LoadItemsFromAssets()
    {
        this.TryLoadItems();

        if (this.itemRepository.Items.Count > 0)
            return true;

        return false;
    }

    /// <summary>
    /// Tries the load items.
    /// </summary>
    private void TryLoadItems()
    {
        this.itemRepository.LoadItens();
    }
}
