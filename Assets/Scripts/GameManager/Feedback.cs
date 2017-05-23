using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controls from Feedback window.
/// </summary>
public class Feedback : MonoBehaviour {

	public InputField emailFeed;
	public InputField bodyFeed;
	private MenuStatus menuStatus;

	void Start () {
		menuStatus = GameManager.Instance.menuStatus;
	}

	/// <summary>
	/// Closes feedback window.
	/// </summary>
	public void closeFeedback() {
		menuStatus.close ("Feedback");
		PauseMenu.Instance.CloseFeedback();
	}

	/// <summary>
	/// Open a email aplication on the user's computer.
	/// </summary>
	public void sendEmail(){
		string email = emailFeed.text;
		string subject = "Feedback";
		string body = bodyFeed.text;
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
		closeFeedback();
	}
}
