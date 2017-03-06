using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NameBar : MonoBehaviour {
	public GameObject NameBarUI;
	Text name;

	public NameBar ()
	{
	}

	private static NameBar instance = null;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void Start () {
		name = GetComponentInChildren<Text>();
		string playerName = GameManager.Instance.gameData.PlayerName;
		if (playerName != null || !playerName.Equals ("")) {
			name.text = playerName;
		} else {
			name.text = "No name!";
		}
	}
}