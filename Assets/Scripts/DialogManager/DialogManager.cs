using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manages the dialog system.
/// </summary>
public class DialogManager : MonoBehaviour
{
    public Text[] responses;
    public Color selectedResponseColor;
    public Color normalResponseColor;
    public Text textField;
    public Image box;
    public float letterPause;
    public float timeBetweenChats = 0.25f;

    int avaiableResponses;
    int selectedResponse;
    float time;
    float textTime;
    float guiAlpha;

    Speaker curSpeaker;
    DialogTree curDialog;
    string curMessage;
	string selectFX;
    string typedMessage;

    /// <summary>
    /// Returns true if there is a conversation alredy happening.
    /// </summary>
    public bool IsSpeaking { get; private set; }

    /// <summary>
    /// Try to speak an dialog. If there is a dialog alredy happening, check it's priority and
    /// choose to maintain the one with the higher.
    /// </summary>
    /// <param name="dialog">The dialog to speak.</param>
    /// <param name="speaker">The character that started the conversation.</param>
    /// <returns>True if the new dialog succeded and will be speech. False if it couldn't.</returns>
    public bool Speak(DialogTree dialog, Speaker speaker)
    {
        if (IsSpeaking && dialog.Priority <= curDialog.Priority)
        	return false; 

		WaitUntilItEnds ();
		return StartConversation (dialog, speaker) != 0f;
    }

	IEnumerator WaitUntilItEnds(){
		yield return new WaitForSeconds (EndConversation ());
	}

    /// <summary>
    /// Adds a letter from curMessage to typedMessage every letterPause seconds.
    /// </summary>
    IEnumerator TypeText()
    {
        foreach (char letter in curMessage.ToCharArray())
        {
            typedMessage += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }

    /// <summary>
    /// Fades the GUI from a start to and end value. If the end value is 0, it must
    /// end any dialogs that may be happening.
    /// </summary>
    /// <param name="start">The start value for the fade. 0 for completely gone and 1 for fully on screen.</param>
    /// <param name="end">The end value for the fade. 0 for completely gone and 1 for fully on screen.</param>
    /// <param name="lenght">The time in seconts it takes for the fade to happen.</param>
    IEnumerator GUIFade(float start, float end, float lenght)
    {
        guiAlpha = start;

        for (float i = 0f; i <= 1f; i += Time.deltaTime * (1f / lenght))
        {
            guiAlpha = Mathf.Lerp(start, end, i);
            yield return null;
        }

        guiAlpha = end;
    }

    /// <summary>
    /// Updates the UI color/text every frame.
    /// </summary>
    void UpdateUI()
    {
        textField.text = typedMessage;
        textField.color = new Color(1f, 1f, 1f, guiAlpha);
        box.color = new Color(1f, 1f, 1f, guiAlpha);

        if (Input.GetButtonDown("Down"))
            if (selectedResponse < avaiableResponses - 1)
                SelectResponse(selectedResponse + 1);

        if (Input.GetButtonDown("Up"))
            if (selectedResponse > 0)
                SelectResponse(selectedResponse - 1);
    }

    void Update()
    {
        textTime += Time.deltaTime;

        UpdateUI();

        if (IsSpeaking)
        {
            if (typedMessage == curMessage)
            {
                StopCoroutine("TypeText");

                if (Input.GetButtonDown("Interaction"))
                {
                    if (!curDialog.Head.IsLeaf)
                        Type();
                    else
                        EndConversation();
                }
            }
            else
            {
                if (Input.GetButtonDown("Interaction") && textTime > 0.25f)
                {
                    typedMessage = curMessage;
                    textTime = 0f;
                    StopAllCoroutines();
                    StartCoroutine(GUIFade(guiAlpha, 1, 0.25f));
                }
            }
        }

    }

    /// <summary>
    /// Start fading in the dialog box and show's the messages in it's queue.
    /// </summary>
    /// <param name="dialog">The dialog to start the conversation.</param>
    /// <returns>The time it will take to fully start the conversation.</returns>
    float StartConversation(DialogTree dialog, Speaker speaker)
    {
        if (!IsSpeaking && textTime > 0f)
        {
            IsSpeaking = true;
            textTime = 0f;
		
            curDialog = dialog;
            curSpeaker = speaker;
			curDialog.Start();
			curDialog.Head.speaker = speaker.gameObject.name;
			curDialog.Execute ();
			selectFX = curDialog.Head.SelectFX;
            curMessage = curDialog.Head.Message;
            typedMessage = "";
            StartCoroutine(TypeText());
	
            SetResponses(curDialog.Head);
            SelectResponse(0);

            float fadeStart = 0f;
            float fadeEnd = 1f;
            float fadeLenght = 0.25f;
            StartCoroutine(GUIFade(fadeStart, fadeEnd, fadeLenght));
            return (fadeStart - fadeEnd) / (1f / fadeLenght);
        }
        return 0f;
    }

    /// <summary>
    ///  Fade out the dialog box and stop conversation.
    /// </summary>
    /// <returns>The time it will take to fully end the conversation.</returns>
    float EndConversation()
    {
		curDialog.Execute ();
        curSpeaker.EndConversation(curDialog.Head);
        IsSpeaking = false;
        curDialog = null;
        curMessage = "";
        textTime = -timeBetweenChats;
        StopCoroutine("TypeText");

        float fadeStart = 1f;
        float fadeEnd = 0f;
        float fadeLenght = 0.25f;
        StartCoroutine(GUIFade(fadeStart, fadeEnd, fadeLenght));
        return (fadeStart - fadeEnd) / (1f / fadeLenght);
    }

    /// <summary>
    /// Traverse the tree to one of the curNode childrens depending on the chosed response from UI.
    /// </summary>
    void Type()
    {
		curDialog.Execute ();
        curDialog.GoToChild(selectedResponse);
		selectFX = curDialog.Head.SelectFX;
        curMessage = curDialog.Head.Message;
        typedMessage = "";
        SetResponses(curDialog.Head);
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// Sets the avaiable responses in the UI.
    /// </summary>
    /// <param name="">A DialogTreeNode, containing all it's children nodes, each having a specific response to be reached.</param>
    void SetResponses(DialogTreeNode node)
    {
		
        avaiableResponses = node.AvaiableChildren;
	
        int resp = 0;
        for(int i = 0; i < node.Children.Count; i++)
        {
            if (node.Children[i].IsAvaiable())
                responses[resp++].text = node.Children[i].Response;
        }

        for (int i = resp; i < responses.Length; ++i)
            responses[i].text = "";

        selectedResponse = 0;

        SelectResponse(selectedResponse);
    }

    /// <summary>
    /// Select a new response from the list and change the UI color for the text UI to show it's selected.
    /// </summary>
    /// <param name="response">The index of the response to select in the response array. Range from 0 to responses.Length.</param>
    void SelectResponse(int response)
    {
        for(int i = 0; i < responses.Length; ++i)
            responses[i].color = normalResponseColor;

        selectedResponse = response;

		responses[selectedResponse].color = selectedResponseColor;

		MusicPlayer.Instance.PlayFX (selectFX);
    }
}