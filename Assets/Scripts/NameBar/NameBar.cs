using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NameBar : MonoBehaviour {
	public GameObject NameBarUI;
	Text pName;

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
		pName = GetComponentInChildren<Text>();
		string playerName = GameManager.Instance.gameData.PlayerName;
		if (!String.IsNullOrEmpty(playerName)) {
			pName.text = playerName;
		} else {
			pName.text = "peaonunes™";
		}
	}
}