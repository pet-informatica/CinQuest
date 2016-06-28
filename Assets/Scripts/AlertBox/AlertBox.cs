using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Developed by: Peão(rngs);
/// Generic Alert box. This class has the porpuse to be invoke anywhere in the code to show some alert!
/// </summary>
public class AlertBox : MonoBehaviour
{
	public AudioClip alertSound;
	public Color titleColor;
	public Color textColor;
	public Text textTitle;
	public Image alertBox;
	public Text alertText;
	public GameObject alertBoxObject;
	public bool isOpen { get; private set; }

	private static AlertBox instance = null;
	private float guiAlpha;

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
			UpdateAlertBoxContent ("Default", "None...");
			isOpen = false;
			alertBoxObject.SetActive (false);
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		alertBoxObject.SetActive (false);
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){
		if (!isOpen)
			alertBoxObject.SetActive (isOpen);
	}

	/// <summary>
	/// Opens the window.
	/// </summary>
	public void OpenWindow(string title, string message)
	{
		if (!checkWindowStatus () && checkIfAlertBoxIsOnScene()) {
			isOpen = true;
			alertBoxObject.SetActive(isOpen);
			StartCoroutine(GUIFade(1f, 0f, 0.01f));
			UpdateAlertBoxContent(title, message);
		}
	}

	/// <summary>
	/// Closes the info.
	/// </summary>
	public void CloseInfo()
	{
		if (checkWindowStatus () && checkIfAlertBoxIsOnScene()) {
			StartCoroutine(GUIFade(1f, 0f, 0.01f));
			UpdateAlertBoxContent ("Default", "None...");
			isOpen = false;
			alertBoxObject.SetActive(isOpen);
		}
	}

	/// <summary>
	/// Checks if alert box is on scene.
	/// </summary>
	/// <returns><c>true</c>, if if alert is on scene, <c>false</c> otherwise.</returns>
	private bool checkIfAlertBoxIsOnScene(){
		if (alertBoxObject == null)
			throw new NullReferenceException ();
		return true;
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

	/// <summary>
	/// Fades the GUI from a start to and end value. If the end value is 0, it must
	/// end any dialogs that may be happening.
	/// </summary>
	/// <param name="start">The start value for the fade. 0 for completely gone and 1 for fully on screen.</param>
	/// <param name="end">The end value for the fade. 0 for completely gone and 1 for fully on screen.</param>
	/// <param name="lenght">The time in seconts it takes for the fade to happen.</param>
	private IEnumerator GUIFade(float start, float end, float lenght)
	{
		this.guiAlpha = start;

		for (float i = 0f; i <= 1f; i += Time.deltaTime * (1f / lenght))
		{
			this.guiAlpha = Mathf.Lerp(start, end, i);
			yield return null;
		}

		this.guiAlpha = end;
	}
}