using UnityEngine;
using System.Collections;

/// <summary>
/// Plays an FX sound every X seconds
/// </summary>
public class FXRepeater : MonoBehaviour {

	public string fx;
	public float time;

	float cTime;

	void Start () {
		cTime = 0f;
	}

	void Update () {
		cTime += Time.deltaTime;
		if (cTime >= time) {
			cTime = 0f;
			MusicPlayer.Instance.PlayFX (fx);
		}
	}
}
