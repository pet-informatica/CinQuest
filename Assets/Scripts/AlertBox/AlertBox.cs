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
    public Image closeBtn;
    public Image closeRing;
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
            isOpen = true;
            StartCoroutine(CloseBox(0.01f));
		}
	}

	/// <summary>
	/// Opens the window.
	/// </summary>
	public void OpenWindow(string title, string message)
	{
		if (!checkWindowStatus () && checkIfAlertBoxIsOnScene()) {
            isOpen = true;
			alertBoxObject.SetActive(isOpen);
            UpdateAlertBoxContent(title, message);
            StartCoroutine(OpenBox(0.25f));
        }
	}

    /// <summary>
    /// Updates this instance 
    /// </summary>
    void Update()
    {
        textTitle.color = new Color(1f, 1f, 1f, guiAlpha);
        alertBox.color = new Color(1f, 1f, 1f, guiAlpha);
        alertText.color = new Color(1f, 1f, 1f, guiAlpha);
        closeBtn.color = new Color(1f, 1f, 1f, guiAlpha);
        closeRing.color = new Color(1f, 1f, 1f, guiAlpha);
    }

	/// <summary>
	/// Closes the info.
	/// </summary>
	public void CloseInfo()
	{
        if (checkWindowStatus () && checkIfAlertBoxIsOnScene()) {
            StartCoroutine(CloseBox(0.25f));  
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

    /// <summary>
    /// Open the UI alert box
    /// </summary>
    /// <param name="length">The time in seconds it will take for the box to fade in</param>
    /// <returns></returns>
    private IEnumerator OpenBox(float length)
    {
        StartCoroutine(GUIFade(0.0f, 1.0f, length));
        yield return null;
    }

    /// <summary>
    /// Waits for the GUIFade to completely take box out of screen, and then set it's gameobject o inactive.
    /// </summary>
    /// <param name="length">The time it will take in seconds for fading out.</param>
    /// <returns></returns>
    private IEnumerator CloseBox(float length)
    {
        isOpen = false;
        StartCoroutine(GUIFade(1.0f, 0.0f, length));
        yield return new WaitForSeconds(length);
        alertBoxObject.SetActive(isOpen);
    }
}