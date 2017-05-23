using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// Used for enabling/disabling npc dynamically through the quests.
/// </summary>
public class NPCListener : MonoBehaviour {

	static NPCListener instance = null;
	/// <summary>
	/// The true instance for static accesing this class resources.
	/// </summary>
	public static NPCListener Instance
	{
		get { return instance; }
	}

	HashSet<string> toEnable = new HashSet<string> ();
	HashSet<string> toDisable = new HashSet<string> ();

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Start(){
		
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Process ();
	}

	/// <summary>
	/// Adds a npc group a name in the list of toEnable group for enabling latter
	/// </summary>
	/// <param name="name">Name.</param>
	public void Enable(string name){
		toEnable.Add (name);
		if (toDisable.Contains (name)) {
			toDisable.Remove (name);
		}
	}


	/// <summary>
	/// Adds a npc group a name in the list of toDisable group for disabling latter
	/// </summary>
	/// <param name="name">Name.</param>
	public void Disable(string name){
		toDisable.Add (name);
		if (toEnable.Contains (name)) {
			toEnable.Remove (name);
		}
	}

	/// <summary>
	/// Adds a npc group a name in the list of toEnable and enables it instantly
	/// </summary>
	/// <param name="name">Name.</param>
	public void EnableInstantly(string name){
		Enable (name);
		GameObject go = GameObject.Find (name);
		if (go != null)
			go.SetActive (true);
	}

	/// <summary>
	/// Adds a npc group a name in the list of toDisable and disables it instantly
	/// </summary>
	/// <param name="name">Name.</param>
	public void DisableInstantly(string name){
		Disable (name);
		GameObject go = GameObject.Find (name);
		if (go != null)
			go.SetActive (false);
	}

	/// <summary>
	/// Flushes the hash set, enabling all gameobjects toEnable and disabling all gameobjects toDisable
	/// </summary>
	void Process(){
		foreach (string name in toEnable) {
			GameObject go = GameObject.Find (name);
			if (go != null) 
				go.SetActive (true);
			
				
		}
		foreach (string name in toDisable) {
			GameObject go = GameObject.Find (name);
			if (go != null)
				go.SetActive (false);
		}
	}
}
