using System;
using UnityEngine;

/// <summary>
/// Developed by: Higor (hcmb)
/// A DialogTree is a complex structure useful for creating dialogs across in game characters.
/// It is composed of a DialogTreeNode, the root, and a EDialogPriority for setting it's priority
/// over other dialogs.
/// </summary>
[CreateAssetMenu]
[Serializable]
public class DialogTree : ScriptableObject
{
    [SerializeField]
    EDialogPriority priority;
    /// <summary>
    /// The priority wich this dialog must have over the others.
    /// </summary>
    public EDialogPriority Priority
    {
        get { return priority; }
    }

    [SerializeField]
    DialogTreeNode root;

    DialogTreeNode head;
    /// <summary>
    /// The head of the tree. Can have at most 4 children.
    /// </summary>
    public DialogTreeNode Head
    {
        get { return head; }
    }

    /// <summary>
    /// Set's the head of the tree back to it's original position.
    /// </summary>
    public void Start()
    {
        head = root;
    }

    /// <summary>
    /// Go down a level in the tree for one of it's childs.
    /// </summary>
    /// <param name="response">The index of the child.</param>
    public void GoToChild(int response)
    {
        if(head.FetchAvaiable(response) != null)
        {
            head = head.FetchAvaiable(response);
            head.Execute();
        }
    }
}
