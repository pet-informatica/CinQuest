using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// A dialog is a simple tuple composed of a list of strings, the messages in wich this dialog must show,
/// and a EDialogPriority, the priority wich it has over the others.
/// </summary>
[System.Serializable]
public class Dialog  {
    public List<string> messages;
    public EDialogPriority priority;
}
