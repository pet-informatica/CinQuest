using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Access of save data of the game.
/// </summary>
public class DataAccess
{
	[DllImport("__Internal")]
	private static extern void SyncFiles();

	[DllImport("__Internal")]
	private static extern void WindowAlert(string message);

	/// <summary>
	/// Saves the game in GameData.dat file.
	/// </summary>
	/// <param name="gameData">Game data.</param>
	public static void SaveGame(GameData gameData) {
		string dataPath = string.Format("{0}/GameData.dat", Application.persistentDataPath);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream fileStream;

		try
		{
			if (File.Exists(dataPath))
			{
				File.WriteAllText(dataPath, string.Empty);
				fileStream = File.Open(dataPath, FileMode.Open);
			}
			else
			{
				fileStream = File.Create(dataPath);
			}

			binaryFormatter.Serialize(fileStream, gameData);
			fileStream.Close();

			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				SyncFiles();
			}
		}
		catch (Exception e)
		{
			PlatformSafeMessage("Failed to Save: " + e.Message);
		}
	}

	/// <summary>
	/// Load the game saved at GameData.dat file.
	/// </summary>
	public static GameData Load()
	{
		GameData gameData = null;
		string dataPath = string.Format("{0}/GameData.dat", Application.persistentDataPath);

		try
		{
			if (File.Exists(dataPath))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(dataPath, FileMode.Open);

				gameData = (GameData)binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
			}
		}
		catch (Exception e)
		{
			PlatformSafeMessage("Failed to Load: " + e.Message);
		}

		return gameData;
	}

	/// <summary>
	/// Determines if the file GameData.dat exist.
	/// </summary>
	/// <returns><c>true</c> if can load game; otherwise, <c>false</c>.</returns>
	public static bool CanLoadGame() {
		string dataPath = string.Format("{0}/GameData.dat", Application.persistentDataPath);
		return File.Exists (dataPath);
	}

	/// <summary>
	/// Deletes the game saved at GameData.dat file.
	/// </summary>
	public static void DeleteSavedData() {
		string dataPath = string.Format("{0}/GameData.dat", Application.persistentDataPath);
		File.Delete(dataPath);
	}

	/// <summary>
	/// Platforms the safe message.
	/// </summary>
	/// <param name="message">Message.</param>
	private static void PlatformSafeMessage(string message)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			WindowAlert(message);
		}
		else
		{
			Debug.Log(message);
		}
	}
}

