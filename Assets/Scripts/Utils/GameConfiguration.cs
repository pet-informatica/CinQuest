using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Developed by: Peao (rngs);
/// GameConfiguration: Represents all configurations properties of the Game.
/// </summary>
public class GameConfiguration
{
	private EDatabaseStorageType _databaseType;
	private string _questCollectionPath = "";
	private string _preConditionCollectionPath = "";

	public EDatabaseStorageType databaseType { get { return this._databaseType; } }

	public string questCollectionPath { get { return this._questCollectionPath; } }

	public string preConditionCollectionPath { get { return this._preConditionCollectionPath;}}

	public GameConfiguration ()
	{
		this.loadConfigurationClass ();
	}

	/// <summary>
	/// Loads the configuration class.
	/// </summary>
	private void loadConfigurationClass ()
	{
		this._databaseType = this.loadDatabaseType (GameConstants.APP_DATABASE_TYPE);
		this.buildCollectionsPath ();
	}

	/// <summary>
	/// Loads the type of the database.
	/// </summary>
	/// <returns>The database type.</returns>
	/// <param name="type">Type.</param>
	private EDatabaseStorageType loadDatabaseType (string type)
	{
		if (type == null)
			return EDatabaseStorageType.unknown;

		if (type.Equals (""))
			return EDatabaseStorageType.unknown;

		switch (type) {
		case "XML":
			return EDatabaseStorageType.XML;
		default:
			return EDatabaseStorageType.XML;
		}
	}

	/// <summary>
	/// Builds the quest collection path.
	/// </summary>
	private void buildCollectionsPath ()
	{
		//TODO: Certify about this.
		string app = Application.dataPath;

		string q = GameConstants.QUEST_COLLECTION_PATH;
		this._questCollectionPath = app + q;

		string p = GameConstants.PRECONDITION_COLLECTION_PATH;
		this._preConditionCollectionPath = app + p;
	}
}
