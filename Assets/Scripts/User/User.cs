using System;
using UnityEngine;
using System.Collections.Generic;

public class User  : MonoBehaviour 
{
    static User instance = null;
    /// <summary>
    /// The user static instance for acessing it's content from the outside.
    /// </summary>
    public static User Instance
    {
        get { return instance; }
    }

	string nick;
    /// <summary>
    /// The nickname for this player.
    /// </summary>
    public string Nick
    {
        get { return nick; }
    }

    Dictionary<int, Quest> quests;
    /// <summary>
    /// A dictionary indexed by quest identifier containing all the user quests.
    /// </summary>
    public Dictionary<int, Quest> Quests
    {
        get { return this.quests; }
    }

    List<int> itemsID;
	/// <summary>
    /// A list of integer that contains the identifiers of all the user's items.
    /// </summary>
	public List<int> ItemsID
    {
        get { return this.itemsID; }
    }

	List<GenericItem> items;
	/// <summary>
	/// A list of GenericItem containing all the user items.
	/// </summary>
	/// <value>The items.</value>
	public List<GenericItem> Items 
	{
		get { return this.items; }
	}

	InventoryModel inventory;
	/// <summary>
	/// The inventory model with the lists of items and tokens.
	/// Every method that modifies the inventory status must be called through the InventoryController.
	/// </summary>
	/// <value>The inventory.</value>
	public InventoryModel Inventory 
	{
		get { return this.inventory; }
	}

	void Awake () 
	{
		if (instance == null) {
			this.LoadNewUser();
		}
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	private void LoadNewUser (){
		instance = this;
		nick = "peaonunes";
		quests = new Dictionary<int,Quest>();
		items = new List<GenericItem>();
        itemsID = new List<int>();
        LoadQuestsFromManager();
	}

	private void LoadUserDataFromFile(){
		//TODO: Deserialize user data.
	}

    private void LoadQuestsFromManager()
    {
        QuestManager manager = GameManager.Instance.questManager;
        if(manager != null)
        {
            quests = manager.getQuests();
        }
    }

    public Quest GetQuest(int id)
    {
        if (quests.ContainsKey(id))
            return quests[id];
        return null;
    }
	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void AddItem(GenericItem item) 
	{
		this.items.Add (item);
	}
	/// <summary>
	/// Removes the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public void RemoveItem(GenericItem item)
	{
		this.items.Remove (item);
	}
	/// <summary>
	/// Adds the identifier of a specific item into the User's list of items ids.
	/// </summary>
	/// <param name="itemID">The item's identifier that will be added to the list.</param>
	public void AddItemID(int itemID){
		this.itemsID.Add(itemID);
    }
	/// <summary>
	/// Removes the identifier of a specific item from the User's list of items ids.
	/// </summary>
	/// <param name="itemID">The item's identifier that will be removed from the list.</param>
	public void RemoveItemID(int itemID){
		this.itemsID.Remove(itemID);
	}
}