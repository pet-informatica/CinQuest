using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool paused = false;

	void Start () {
	
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused) {
				GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScreenFader> ().BeginFade (-1);
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (false);
				paused = false;
			} else {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (true);
				GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScreenFader> ().BeginFade (1);
				paused = true;
			}
		}
	}
}
