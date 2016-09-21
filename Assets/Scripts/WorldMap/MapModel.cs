using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
///	This model holds the gameobjects of all the maps the world map can render.
/// Developed by: Higor (hcmb)
/// </summary>
[Serializable]
public class MapModel : MonoBehaviour {
	/// <summary>
	/// A dummy class to encapsulate the info needed to insert maps in the dictionary. 
	/// We need it for being able to populate the model from inspector.
	/// </summary>
	[Serializable]
	public class MapStage
	{
		public string hash;
		public string parent;
		public GameObject stage;
	}
	public MapStage[] stages;

	/// <summary>
	/// A dummy class to encapsulate the info needed to insert maps in the dictionary. 
	/// We need it for being able to populate the model from inspector.
	/// </summary>
	[Serializable]
	public class MapObject
	{
		public string hash;
		public string title;
	}
	public MapObject[] objects;

	/// <summary>
	/// Parses the names coming from SCENES to formal hash names of map stages that should appear in that scene
	/// when player opens the world map. A dummy class to be able to populate the model from inspector.
	/// </summary>
	[Serializable]
	public class SceneToStage
	{
		public string from;
		public string to;
	}
	public SceneToStage[] hashNames;

	Dictionary<string, MapStage> stageModel;
	Dictionary<string, MapObject> objectModel;
	Dictionary<string, string> nameParser;

	/// <summary>
	/// Populates the intern dictionary using the external entrances from inspector;
	/// </summary>
	public void Populate()
	{
		stageModel = new Dictionary<string, MapStage> ();
		for (int i = 0; i < stages.Length; i++) 
			stageModel.Add (stages [i].hash, stages [i]);

		nameParser = new Dictionary<string, string> ();
		for (int i = 0; i < hashNames.Length; i++)
			nameParser.Add (hashNames [i].from, hashNames [i].to);

		objectModel = new Dictionary<string, MapObject> ();
		for (int i = 0; i < objects.Length; i++)
			objectModel.Add (objects [i].hash, objects [i]);
	}

	/// <summary>
	/// Search for an Stage in the WorldMap model by the name of the current scene or formal hash name.
	/// If it receives a scene name, it is converted to a formal WorldMap hash entry.
	/// </summary>
	/// <param name="hash">The name of the current scene, or the formal map name.</param>
	public MapStage FindStage(string hash)
	{
		if (!nameParser.ContainsKey(hash) || !stageModel.ContainsKey (nameParser[hash]) )
			return null;
		return stageModel [nameParser[hash]];
	}

	/// <summary>
	/// Search for an small map object
	/// </summary>
	/// <returns>Returns the MapObject associated with the hash entry.</returns>
	/// <param name="hash">The formal hash name of the small object.</param>
	public MapObject FindObject(string hash){
		if (!objectModel.ContainsKey (hash))
			return null;
		return objectModel [hash];
	}
}
