using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Speaker is an abstract class wich contains only a list of dialogs
/// that can be used in order to maintain a dialog. Must be inherited by other
/// classes like InterativeSpeaker for setting the conditions under wich
/// those dialogs are going to show off.
/// </summary>
public abstract class Speaker : MonoBehaviour {
    public List<Dialog> dialogs;
    public int defaultDialogIndex;
    protected DialogManager dialogManager;

    /// <summary>
    /// Creates a new Speaker
    /// </summary>
    /// <param name="dialogs">The list of dialogs it can speak</param>
    public Speaker(List<Dialog> dialogs)
    {
        this.dialogs = dialogs;
        this.defaultDialogIndex = 0;
    }

    void Start()
    {
        this.dialogManager = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
    }

    /// <summary>
    /// Communicates with the DialogManager in the ItemManager to try to start a dialog
    /// </summary>
    /// <param name="dialog">The dialog to speak</param>
    public void Speak(Dialog dialog)
    {
        dialogManager.Speak(dialog);
    }
}
