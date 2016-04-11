using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Developed by: Higor Cavalcanti
/*
    Speaker class is useful for every character that want's to have a conversation with
    the player. With it, you can create a list of messages to be displayed one after
    another, and it will call the GameManager Dialog class to show those messages
    automatically.

    How does it works:
    Just attach it to an object and add a bunch of strings in the inspector.
    It also needs a Collider2D marked as Trigger. When the player is inside it,
    If it press Z, the messages will be shown one after the other.
*/
public class Speaker : MonoBehaviour
{
    Dialog dialog;
    PlayerController player;

    public List<string> dialogs;

    void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Dialog>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (!dialog.IsSpeaking)
            {
                player.InDialog = false;
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    foreach (string message in dialogs)
                        dialog.AddMessage(message);
                    dialog.StartConversation();
                    player.InDialog = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        dialog.EndConversation();
    }
}
