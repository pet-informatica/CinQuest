using UnityEngine;
using System.Collections;

/// <summary>
/// Developed by: Higor (hcmb)
/// A TicketGate can transport characters
/// </summary>
public class TicketGate : MonoBehaviour {

    public TicketGate target;
    public DialogTree noIdDialog;
    Teleporter teleporter;
    Speaker speaker;

    void Start()
    {
        teleporter = gameObject.AddComponent<Teleporter>();
        speaker = gameObject.AddComponent<Speaker>();
        speaker.SetDialog(noIdDialog);
    }

    /// <summary>
    /// Check the PreCondition of Cracha.
    /// </summary>
    /// <returns>True if the player inventory has an IDCard.</returns>
    bool PlayerHasIDCard()
    {
		User currentUser = User.Instance;
		IPreCondition checkCracha = GameManager.Instance.preConditionManager.getPreCondition (6);
		return checkCracha.checkIfMatches (currentUser);
    }

    /// <summary>
    /// Automatically transport the player if it interacting with the TicketGate.
    /// </summary>
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "PlayerFront" && Input.GetKey(KeyCode.Z))
        {
            if (PlayerHasIDCard())
            {
                teleporter.Teleport(collider.transform.parent.gameObject, true, target.transform);
            }
            else
            {
                speaker.Speak();
            }   
        }  
    }
}
