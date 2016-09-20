using UnityEngine;
using System.Collections;

public class Feedback : MonoBehaviour {

	private GameObject feedbackCanvas;

	void Start () {
		feedbackCanvas = GameObject.Find("Feedback Canvas");
	}

	void Update () {

	}

	public void closeFeedback() {
		GameObject.Find ("Feedback Canvas").GetComponent<Feedback> ().cancel();
	}

	private void cancel(){
		feedbackCanvas.SetActive (false);
	}
}
