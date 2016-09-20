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
		GameObject.Find ("PauseManager").GetComponent<Feedback> ().cancel();
	}

	private void cancel(){
		feedbackCanvas.SetActive (false);
	}

	public void emailFeedback() {
		GameObject.Find ("PauseManager").GetComponent<Feedback> ().sendEmail();
	}

	private void sendEmail(){
		//email Id to send the mail to
		string email = "rcac@cin.ufpe.br";
		//subject of the mail
		string subject = "Feedback";
		//body of the mail which consists of Device Model and its Operating System
		string body = "Teste CInQuest";
		//Open the Default Mail App
		Application.OpenURL ("mailto:" + email + "?subject=" + subject + "&body=" + body);
		print ("enviou???");

	}
}
