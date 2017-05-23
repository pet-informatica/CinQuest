using UnityEngine;
using System.Collections;

/// <summary>
/// This NPC is responsible for teleporting the player on the ticket gates.
/// </summary>
public class Gatekeeper : InterativeSpeaker
{
    public Transform origin;
    public Transform destine;

    protected Teleporter teleporter;
	protected bool playerInside;


	void Start ()
    {
        this.dialogManager = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
        teleporter = gameObject.AddComponent<Teleporter>();
	}

    public override void EndConversation(DialogTreeNode endingNode)
    {
		if (endingNode.Response == "Perdi o crachá!") {
			if (playerInside)
				teleporter.Teleport (player, true, destine);
			else
				teleporter.Teleport (player, true, origin);
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
