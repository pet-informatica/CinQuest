using UnityEngine;
using System.Collections.Generic;

public class NPCListener : MonoBehaviour {

	static NPCListener instance = null;
	/// <summary>
	/// The true instance for static accesing this class resources.
	/// </summary>
	public static NPCListener Instance
	{
		get { return instance; }
	}

	public NPCGroup[] groups;
	Dictionary<string, int> index;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
		index = new Dictionary<string, int> ();

		for(int i = 0; i < groups.Length; ++i){
			index.Add (groups [i].name, i);
		}
	}
	
	public void Instantiate(string name){
		foreach (GameObject go in groups[index[name]].npcs) {
			go.SetActive (true);
		}
	}

	public void Disable(string name){
		foreach (GameObject go in groups[index[name]].npcs) {
			go.SetActive (false);
		}
	}
}
