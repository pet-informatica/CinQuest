using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// A RewardTreeNode will have a list of rewards that will be given
/// for the player when this node is reached.
/// </summary>
[CreateAssetMenu]
[Serializable]
public class RewardTreeNode : DialogTreeNode
{
    [SerializeField]
    List<int> rewardIDs;
    /// <summary>
    /// The list of the reward item ID's that the user will win after reaching this node
    /// </summary>
    public List<int> RewardIDs
    {
        get { return rewardIDs; }
    }

    /// <summary>
    /// When the node is reached, gives a list of rewards for the player
    /// </summary>
    public override void Execute()
    {
        User user = User.Instance;

        foreach(int id in RewardIDs)
        {
            user.AddItem(GameManager.Instance.itemManager.GetItem(id));
        }
    }
}
