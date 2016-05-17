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

	public EDatabaseStorageType databaseType { get { return _databaseType; } }

	public string questCollectionPath { get { return _questCollectionPath; } }

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
		this.buildQuestCollectionPath ();
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
	private void buildQuestCollectionPath ()
	{
		//TODO: Certify about this.
		string p1 = Application.dataPath;
		string p2 = GameConstants.QUEST_COLLECTION_PATH;
		try {
			string combination = Path.Combine (p1, p2);

			this._questCollectionPath = combination;
		} catch (Exception e) {
			if (p1 == null)
				p1 = "null";
			if (p2 == null)
				p2 = "null";
			Console.WriteLine ("You cannot combine '{0}' and '{1}' because: {2}{3}",
				p1, p2, Environment.NewLine, e.Message);
		}
	}
}
