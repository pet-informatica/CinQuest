using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AlertBox : MonoBehaviour
{
	public AudioClip alertSound;
	public Color titleColor;
	public Color textColor;
	public Text textTitle;
	public Image alertBox;
	public Text alertText;

	private GameObject uiBox;

	public bool isOpen { get; private set; }

	static AlertBox instance = null;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static AlertBox Instance
	{
		get { return instance; }
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () 
	{
		if (instance == null) {
			instance = this;
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		uiBox = GameObject.FindGameObjectWithTag("AlertBox").GetComponent<GameObject>();
		if (uiBox != null) {
			isOpen = false;
			uiBox.SetActive (false);
			foreach (GameObject g in uiBox.GetComponentsInChildren<GameObject>())
				g.SetActive (false);
		}
	}

	/// <summary>
	/// Opens the window.
	/// </summary>
	public void OpenWindow(string title, string message)
	{
		if (!checkWindowStatus ()) {
			UpdateAlertBoxContent(title, message);
			isOpen = true;
			uiBox.SetActive(isOpen);
		}
	}

	/// <summary>
	/// Closes the info.
	/// </summary>
	public void CloseInfo()
	{
		if (checkWindowStatus ()) {
			UpdateAlertBoxContent ("Default", "None...");
			isOpen = false;
			uiBox.SetActive(isOpen);
		}
	}

	/// <summary>
	/// Checks the window status.
	/// </summary>
	/// <returns><c>true</c>, if window status is Open, <c>false</c> otherwise.</returns>
	private bool checkWindowStatus(){
		return isOpen;
	}
		
	/// <summary>
	/// Updates the content of the alert box.
	/// </summary>
	/// <param name="title">Title.</param>
	/// <param name="message">Message.</param>
	private void UpdateAlertBoxContent(string title, string message){
		if (title == null || message == null || title.Equals ("") || message.Equals (""))
			throw new ArgumentException ("Title or message invalid!");
		textTitle.text = title;
		alertText.text = message;
	}

}

