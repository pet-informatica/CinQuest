using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Feedback : MonoBehaviour {

	private GameObject feedbackCanvas;
	private Feedback feedback;
	public InputField emailFeed;
	public InputField bodyFeed;

	void Start () {
		feedbackCanvas = GameObject.Find("Feedback Canvas");
		feedback = feedbackCanvas.GetComponent<Feedback> ();
	}

	void Update () {
	}

	public void closeFeedback() {
		feedback.cancel();
	}

	private void cancel(){
		feedbackCanvas.SetActive (false);
	}

	public void emailFeedback() {
		feedback.sendEmail();
	}

	private void sendEmail(){
		
		//email Id to send the mail to
		string email = emailFeed.text;
		//subject of the mail
		string subject = "Feedback";
		//body of the mail which consists of Device Model and its Operating System
		string body = bodyFeed.text;
		print (email + "/" + body);
		//Open the Default Mail App
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
		print ("enviou???");

	}
}
