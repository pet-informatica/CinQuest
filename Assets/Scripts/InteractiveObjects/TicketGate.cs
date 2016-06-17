using UnityEngine;
using System.Collections;

/// <summary>
/// Developed by: Higor (hcmb)
/// A TicketGate can transport characters
/// </summary>
public class TicketGate : MonoBehaviour {

    public TicketGate target;
    public DialogTree noIdDialog;
    Speaker speaker;
    ScreenFader fader;
    bool transporting;

    void Start()
    {
        fader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScreenFader>();
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
		IPreCondition checkCracha = GameManager.Instance.preConditionManager.getPreCondition (1);
		return checkCracha.checkIfMatches (currentUser);
    }

    /// <summary>
    /// Call fade in and sleep until it's done.
    /// </summary>
     IEnumerator TeleportRoutine(GameObject chatacter, bool fade)
    {
        transporting = true;

        if (fade)
            yield return new WaitForSeconds(fader.BeginFade(1, 0.3f));

        chatacter.transform.position = target.transform.position;

        if (fade)
         fader.BeginFade(-1, 0.3f);

        transporting = false;
    }

    /// <summary>
    /// Teleports a character for the other side of the gate.
    /// </summary>
    /// <param name="character">The character gameObject you want to transport.</param>
    /// <param name="fade">Set true if you want to fade the screen in the process.</param>
    /// <returns>True if the teleport was sucessful. Flase otherwise if it couldn't happen.</returns>
    public bool Teleport(GameObject character, bool fade)
    {
        if(transporting)
            return false;
        StartCoroutine(TeleportRoutine(character, true));
        return true;
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
                Teleport(collider.transform.parent.gameObject, true);
            }
            else
            {
                speaker.Speak();
            }   
        }  
    }
}
