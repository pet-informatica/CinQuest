using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public AudioClip sound;
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

    DialogTree curDialog;
    DialogTreeNode curNode;
    string curMessage;
    string typedMessage;

    /// <summary>
    /// Returns true if there is a conversation alredy happening
    /// </summary>
    public bool IsSpeaking { get; private set; }

    /// <summary>
    /// Try to speak an dialog. If there is a dialog alredy happening, check it's priority and
    /// choose to maintain the one with the higher.
    /// </summary>
    /// <param name="dialog">The dialog to speak</param>
    /// <returns>True if the new dialog succeded and will be speech. False if it couldn't.</returns>
    public bool Speak(DialogTree dialog)
    {
        if (IsSpeaking && dialog.priority <= curDialog.priority)
        return false; 

        ClearDialogBox();
        StartConversation(dialog);
        return true;
    }

    /// <summary>
    /// Erase the messages and fade out the GUI. Blocks the method that calls it until it's
    /// done and it's safe for progressing.
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearDialogBox()
    {
        yield return new WaitForSeconds(EndConversation());
    }

    /// <summary>
    /// Adds a letter from curMessage to typedMessage every letterPause seconds
    /// </summary>
    IEnumerator TypeText()
    {
        foreach (char letter in curMessage.ToCharArray())
        {
            typedMessage += letter;
            if (sound)
                GetComponent<AudioSource>().PlayOneShot(sound);
            yield return new WaitForSeconds(letterPause);
        }
    }

    /// <summary>
    /// Fades the GUI from a start to and end value. If the end value is 0, it must
    /// end any dialogs that may be happening.
    /// </summary>
    /// <param name="start">The start value for the fade. 0 for completely gone and 1 for fully on screen.</param>
    /// <param name="end">The end value for the fade. 0 for completely gone and 1 for fully on screen.</param>
    /// <param name="lenght">The time in seconts it takes for the fade to happen</param>
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

        if (Input.GetKeyDown(KeyCode.DownArrow))
            if (selectedResponse < avaiableResponses - 1)
                SelectResponse(selectedResponse + 1);

        if (Input.GetKeyDown(KeyCode.UpArrow))
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

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (curNode.children.Count > 0)
                        Type();
                    else
                        EndConversation();
                }
            }
            else
            {
                if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)) && textTime > 0.25f)
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
    /// Start fading in the dialog box and show's the messages in it's queue
    /// </summary>
    /// <param name="dialog">The dialog to start the conversation</param>
    /// <returns>The time it will take to fully start the conversation</returns>
    float StartConversation(DialogTree dialog)
    {
        if (!IsSpeaking && textTime > 0f)
        {
            IsSpeaking = true;
            textTime = 0f;

            curDialog = dialog;
            curNode = curDialog.root;
            curMessage = curNode.message;
            typedMessage = "";
            StartCoroutine(TypeText());
            SetResponses(curNode);
            SelectResponse(0);

            float fadeStart = 0f;
            float fadeEnd = 1f;
            float fadeLenght = 0.25f;
            StartCoroutine(GUIFade(fadeStart, fadeEnd, fadeLenght));
            return (fadeStart - fadeEnd) / (1f / fadeLenght);
        }
        return 0;
    }

    /// <summary>
    ///  Fade out the dialog box and stop conversation
    /// </summary>
    /// <returns>The time it will take to fully end the conversation</returns>
    float EndConversation()
    {
        IsSpeaking = false;
        curDialog = null;
        curNode = null;
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
    /// Traverse the tree to one of the curNode childrens depending on the chosed response from UI
    /// </summary>
    void Type()
    {
        curNode = curNode.children[selectedResponse];
        curMessage = curNode.message;
        typedMessage = "";
        SetResponses(curNode);
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// Sets the avaiable responses in the UI
    /// </summary>
    /// <param name="">A DialogTreeNode, containing all it's children nodes, each having a specific response to be reached.</param>
    void SetResponses(DialogTreeNode node)
    {
        avaiableResponses = node.children.Count;

        for(int i = 0; i < avaiableResponses; ++i)
            responses[i].text = node.children[i].response;

        for (int i = avaiableResponses; i < responses.Length; ++i)
            responses[i].text = "";

        selectedResponse = 0;
        SelectResponse(selectedResponse);
    }

    /// <summary>
    /// Select a new response from the list and change the UI color for the text UI to show it's selected
    /// </summary>
    /// <param name="response">The index of the response to select in the response array. Range from 0 to responses.Length.</param>
    void SelectResponse(int response)
    {
        for(int i = 0; i < responses.Length; ++i)
            responses[i].color = normalResponseColor;

        selectedResponse = response;
        responses[selectedResponse].color = selectedResponseColor;
    }
}