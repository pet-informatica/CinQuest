using System;

/*
 *  Developed by Peao (rngs);
 * 
 * 	Represents all configurations properties of the Game.
 * 
 * */

public class GameConfiguration
{
	private EDatabaseStorageType _databaseType;

	public EDatabaseStorageType databaseType { get { return _databaseType; } }

	public GameConfiguration ()
	{
		this.loadConfigurationFile ();
	}

	private void loadConfigurationFile(){
		// TODO: Implement a method to load from "app.config" file the Game configuration such as database type and so on... 
		this._databaseType = EDatabaseStorageType.XML;
	}
}
