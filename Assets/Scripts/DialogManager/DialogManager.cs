using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour
{
    public Texture textBaloon;
    public AudioClip sound;
    public float letterPause;
    public float timeBetweenChats = 0.5f;
    float time;
    bool show;
    float textTime;
    float guiAlpha;

    Dialog currentDialog;
    Queue<string> message = new Queue<string>();
    string curMessage;
    string typedMessage;

    Text text;
    Image box;

    void Awake()
    {
        text = GameObject.Find("DialogBox").GetComponentInChildren <Text>();
        box = GameObject.Find("DialogBox").GetComponentInChildren<Image>();
    }

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
    public bool Speak(Dialog dialog)
    {
        if (IsSpeaking && dialog.priority <= currentDialog.priority)
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
    /// Types a string message letter by letter.
    /// </summary>
    /// <returns></returns>
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
    /// <returns></returns>
    IEnumerator GUIFade(float start, float end, float lenght)
    {
        guiAlpha = start;
        for (float i = 0f; i <= 1f; i += Time.deltaTime * (1f / lenght))
        {
            guiAlpha = Mathf.Lerp(start, end, i);
            yield return null;
        }

        guiAlpha = end;

        if (guiAlpha == 0f)
            End();
    }

    void Update()
    {
        textTime += Time.deltaTime;
        text.text = typedMessage;
        text.color = new Color(1f, 1f, 1f, guiAlpha);
        box.color = new Color(1f, 1f, 1f, guiAlpha);
        if (IsSpeaking)
        {
            if (typedMessage == curMessage)
            {
                StopCoroutine("TypeText");

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (message.Count > 0)
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
    float StartConversation(Dialog dialog)
    {
        if (!IsSpeaking && textTime > 0f)
        {
            currentDialog = dialog;

            foreach (string m in dialog.messages)
                message.Enqueue(m);

            float fadeStart = 0f;
            float fadeEnd = 1f;
            float fadeLenght = 0.5f;
            StartCoroutine(GUIFade(fadeStart, fadeEnd, fadeLenght));
            textTime = 0f;
            Type();
            IsSpeaking = true;
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
        StopCoroutine("TypeText");
        currentDialog = null;
        textTime = -timeBetweenChats;
        curMessage = "";
        message.Clear();
        IsSpeaking = false;
        float fadeStart = 1f;
        float fadeEnd = 0f;
        float fadeLenght = 0.5f;
        StartCoroutine(GUIFade(fadeStart, fadeEnd, fadeLenght));
        return (fadeStart - fadeEnd) / (1f / fadeLenght);
    }

    /// <summary>
    /// Pop the message queue in order to show the top message
    /// </summary>
    void Type()
    {
        curMessage = message.Peek();
        message.Dequeue();
        typedMessage = "";
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// Stop all coroutines and clear variables for ending the dialog
    /// </summary>
    void End()
    {
        typedMessage = "";
        StopCoroutine("TypeText");
        currentDialog = null;
        textTime = -timeBetweenChats;
        curMessage = "";
        message.Clear();
        IsSpeaking = false;
    }
}