using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Developed by: Higor (hcmb)
/// A DialogTree is composed of an EDialogPriority and a root node of DialogTreeNode.
/// </summary>
[CreateAssetMenu]
[Serializable]
public class DialogTree : ScriptableObject
{
    public EDialogPriority priority;
    public DialogTreeNode root;
}
