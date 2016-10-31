using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Speaker is an abstract class wich contains only a list of dialog trees
/// that can be used in order to maintain a dialog. Must be inherited by other
/// classes like InterativeSpeaker for setting the conditions under wich
/// those dialogs are going to show off.
/// </summary>
public class Speaker : MonoBehaviour
{
    public List<DialogTree> dialogs;
    public int defaultDialogIndex;
    protected DialogManager dialogManager;

    void Start()
    {
        this.dialogManager = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
    }

    public void SetDialog(DialogTree dialog)
    {
        dialogs = new List<DialogTree>();
        dialogs.Add(dialog);
        defaultDialogIndex = 0;
    }

    /// <summary>
    /// Communicates with the DialogManager in the ItemManager to try to start a dialog.
    /// </summary>
    /// <param name="dialog">The dialog tree to speak.</param>
    public virtual bool Speak()
    {
        return dialogManager.Speak(dialogs[defaultDialogIndex], this);
    }

    /// <summary>
    /// Communicates with the DialogManager in the ItemManager to try to start a dialog.
    /// </summary>
    /// <param name="dialog">The dialog tree to speak.</param>
	public virtual bool Speak(DialogTree dialog)
    {
		return dialogManager.Speak(dialog, this);
    }

    /// <summary>
    /// Called everytime a conversation is finished. Can be extended for executing actions.
    /// </summary>
    /// <param name="endingNode">The last node speaked before the conversation ended.</param>
    public virtual void EndConversation(DialogTreeNode endingNode)
    {
        
    }
}
