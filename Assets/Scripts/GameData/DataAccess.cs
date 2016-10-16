using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataAccess
{
	[DllImport("__Internal")]
	private static extern void SyncFiles();

	[DllImport("__Internal")]
	private static extern void WindowAlert(string message);

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

	public static bool CanLoadGame() {
		string dataPath = string.Format("{0}/GameData.dat", Application.persistentDataPath);
		return File.Exists (dataPath);
	}

	public static void DeleteSavedData() {
		string dataPath = string.Format("{0}/GameData.dat", Application.persistentDataPath);
		File.Delete(dataPath);
	}

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

