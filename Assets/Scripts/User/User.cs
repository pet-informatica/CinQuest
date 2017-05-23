using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represent an user instance. Has a nick, a list of quests, and a list of items.
/// </summary>
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

    List<GenericItem> items;
	/// <summary>
    /// A list of GenericItem containing all the user items.
    /// </summary>
	public List<GenericItem> Items
    {
        get { return this.items; }
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

	/// <summary>
	/// Instantiates a new user instance and load it's quests
	/// </summary>
	private void LoadNewUser (){
		instance = this;
		nick = "peaonunes";
		quests = new Dictionary<int,Quest> ();
        items = new List<GenericItem>();
        LoadQuestsFromManager();
	}

	private void LoadUserDataFromFile(){
		//TODO: Deserialize user data.
	}

	/// <summary>
	/// Get the list os quests from the game manager
	/// </summary>
    private void LoadQuestsFromManager()
    {
        QuestManager manager = GameManager.Instance.questManager;
        if(manager != null)
        {
            quests = manager.getQuests();
        }
    }

	/// <summary>
	/// Get a quest based on it's id
	/// </summary>
	/// <returns>The quest. Null if couldn't find it.</returns>
	/// <param name="id">The quest id.</param>
    public Quest GetQuest(int id)
    {
        if (quests.ContainsKey(id))
            return quests[id];
        return null;
    }

	/// <summary>
	/// Adds and generic item to players list of items
	/// </summary>
	/// <param name="newItem">The generic item to add.</param>
	public void AddItem(GenericItem newItem){
		this.items.Add(newItem);
    }
}