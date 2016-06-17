using System;
using UnityEngine;
using System.Collections.Generic;

public class User  : MonoBehaviour 
{
    static User instance = null;
    public static User Instance
    {
        get { return instance; }
    }


	private string _name;
	private Dictionary<int,Quest> _userQuests;
	private List<GenericItem> _items;

	public string userName { get { return this._name; } }
	public Dictionary<int,Quest> userQuests { get { return this._userQuests; } }
	public List<GenericItem> items { get { return this._items; } }

	void Awake () 
	{
		if (instance == null) {
			this.loadUser ();
		}
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	private void loadUser (){
		instance = this;
		this._name = "peaonunes";
		this._userQuests = new Dictionary<int,Quest> ();
		this._items = new List<GenericItem>();
	}

	private void loadUserDataFromFile(){
		//TODO: Deserialize user data.
	}

    public Quest getQuest(int id)
    {
        if (userQuests.ContainsKey(id))
            return userQuests[id];
        return null;
    }

	public void addItem(GenericItem newItem){
		//TODO: Synchronize the inventory too. This will depend on Lucas activite right now. We should update it later.
		this._items.Add(newItem);
	}
}