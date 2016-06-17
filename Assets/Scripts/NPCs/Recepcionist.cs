using UnityEngine;
using System.Collections;

public class Recepcionist : InterativeSpeaker
{
    public Transform origin;
    public Transform destine;

    Teleporter teleporter;
    bool playerInside;

	void Start ()
    {
        this.dialogManager = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
        teleporter = gameObject.AddComponent<Teleporter>();
	}

    public override void EndConversation(DialogTreeNode endingNode)
    {
        if(endingNode.Response == "Catraca")
        {
            if (playerInside) teleporter.Teleport(player, true, destine);
            else teleporter.Teleport(player, true, origin);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            playerInside = false;
        }   
    }
}
