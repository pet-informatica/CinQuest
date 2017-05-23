using UnityEngine;
using System.Collections;

/// <summary>
/// Autotically start the dialog of the speaker after x seconds in a beginning of a scene.
/// </summary>
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
