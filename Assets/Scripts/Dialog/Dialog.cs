using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Developed by: Higor Cavalcanti
/* 
    Description:
    This class is an generic way of showing text in a dialog box. 
    It works for every situation, and every character that need's
    to speak with the player, or show any text in screen.

    How does it works:
    It has a Queue of messages that can be populated with strings using the method
    AddMessage(string m), and then after all messages were added, you can call StartConversation()
    in order to show them in the screen and start a dialog.
    An example for this is:
    void Start()
    {
        Dialog dialog = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Dialog>();
        dialog.AddMessage("Welcome to the university!");
        dialog.AddMessage("In here, we study a lot, i hope you enjoy it.");
        dialog.AddMessage("See you later, goodbye!");
        dialog.StartConversation();
    }
    This will show the 3 messages one after another.

    What does it needs:
    The dialog must be attached to the GameManager in order to work properly, because it's global, and the
    GameManager is the only real global object we have.
    You need a texture of a rectangle or something similar to be the dialog box. Put it in the textBaloon variable.
    Then a beautiful font for displaying. Put it into GUIStyle variable;
    Optionally, the dialog can make a sound every time a letter is printed. If you like it, find an audioclip for it.
*/

public class Dialog : MonoBehaviour
{
    public Texture textBaloon;
    public AudioClip sound;
    public GUIStyle font;
    public float letterPause;
    public Vector2 textPosition;
    float time;
    bool show;
    float textTime;
    float guiAlpha;

    Queue<string> message = new Queue<string>();
    string curMessage;
    string typedMessage;

    /// <summary>
    /// Returns false if it isn't safe to start another conversation
    /// </summary>
    public bool IsSpeaking {
       get { return message.Count > 0 || show; }
    }
        
    /// <summary>
    /// Add a message to the dialog queue
    /// </summary>
    /// <param name="m">A string representing the message</param>
    public void AddMessage(string m)
    {
        message.Enqueue(m);
    }

    IEnumerator TypeText()
    {
        foreach (char letter in curMessage.ToCharArray())
        {
            typedMessage += letter;
            if (sound)
                GetComponent<AudioSource>().PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }

    IEnumerator GUIFade(float start, float end, float lenght)
    {
        for (float i = 0f; i <= 1f; i += Time.deltaTime * (1f / lenght))
        {
            guiAlpha = Mathf.Lerp(start, end, i);
            yield return null;
        }

        guiAlpha = end;

        if (guiAlpha == 0f)
            End();
    }

    void OnGUI()
    {
        if (show)
        {
            GUI.color = new Color(1f, 1f, 1f, guiAlpha);
          
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / 1280f, Screen.height / 800f, 1.0f));
            GUI.DrawTexture(new Rect(textPosition.x - 165, textPosition.y - 75, 1280, 256), textBaloon);
            GUI.Label(new Rect(textPosition.x, textPosition.y, 950, 256), typedMessage, font);
        }
    }


    void Update()
    {
        textTime += Time.deltaTime;

        if (show)
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
                    StopAllCoroutines();
                    StartCoroutine(GUIFade(guiAlpha, 1, 0.25f));
                }
            }
        }

    }

    void Type()
    {
        curMessage = message.Peek();
        message.Dequeue();
        typedMessage = "";
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// Start fading in the dialog box and show's the messages in it's queue
    /// </summary>
    public void StartConversation()
    {
        if (!show && textTime > 0f)
        {
            StartCoroutine(GUIFade(0, 1, 0.75f));
            show = true;
            textTime = 0f;
            Type();
        }

    }

    /// <summary>
    /// Fade out the dialog box and stop conversation
    /// </summary>
    public void EndConversation()
    {
        StartCoroutine(GUIFade(1, 0, 0.5f));
    }

    void End()
    {
        typedMessage = "";
        StopCoroutine("TypeText");
        show = false;
        textTime = -0.25f;
        message.Clear();
    }

    /// <summary>
    /// Clear the dialog queue
    /// </summary>
    public void Prepare()
    {
        message.Clear();
    }
}