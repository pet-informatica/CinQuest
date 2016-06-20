using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// A ConditionalTreeNode will receive and list of IPreConditions and will only
/// be avaiable to be reached if the player matches all the pre conditions listed;
/// </summary>
[CreateAssetMenu]
[Serializable]
public class ConditionalTreeNode : DialogTreeNode
{
    [SerializeField]
    List<int> preconditionIDs;
    /// <summary>
    /// The list of the Precondition ID's that the user must satisfie in order to reach this node.
    /// </summary>
    public List<int> PreconditionIDs
    {
        get { return preconditionIDs; }
    }

    /// <summary>
    /// Check if all the pre conditions are satisfied.
    /// </summary>
    /// <returns>TRUE if the current node can be reached. False otherwise.</returns>
    public override bool IsAvaiable()
    {
        User user = User.Instance;
        
        foreach(int id in preconditionIDs)
        {
            if (!GameManager.Instance.preConditionManager.getPreCondition(id).checkIfMatches(user))
                return false;
        }

        return true;
    }
}
