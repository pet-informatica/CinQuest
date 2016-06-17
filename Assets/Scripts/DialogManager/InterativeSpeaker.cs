using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// An InterativeSpeaker is a Speaker that starts the dialog when the player presses the interaction button
/// when colliding to it's Trigger2D.
/// </summary>
public class InterativeSpeaker : Speaker
{
    void Start()
    {
        dialogManager = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "PlayerFront")
        {
            if (Input.GetButtonDown("Interaction"))
            {
                if(defaultDialogIndex < dialogs.Count && dialogs[defaultDialogIndex] != null)
                    Speak(dialogs[defaultDialogIndex]);
            }
        }
    }
}
