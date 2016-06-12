using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Developed by: Higor (hcmb)
/// The node of a DialogTree. Contains a message to speak, a response, that is,
/// the response from the player that make us to reach this node, and a list of
/// children nodes to keep tree going.
/// </summary>
[CreateAssetMenu]
[Serializable]
public class DialogTreeNode : ScriptableObject
{
    public string response;
    public string message;
    public List<DialogTreeNode> children;
}