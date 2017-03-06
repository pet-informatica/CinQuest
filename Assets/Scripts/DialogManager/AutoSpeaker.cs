using UnityEngine;
using System.Collections;

public class AutoSpeaker : Speaker {
	public float delay = 5.0f;

	void Start () {
		this.dialogManager = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
		StartCoroutine (S());
	}

	IEnumerator S(){
		yield return new WaitForSeconds (delay);
		Speak(dialogs[defaultDialogIndex]);
	}
}
