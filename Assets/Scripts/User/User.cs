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

	public void AddItem(GenericItem newItem){
		//TODO: Synchronize the inventory too. This will depend on Lucas activite right now. We should update it later.
		this.items.Add(newItem);
    }
}