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
    /// Constructs a new TreeNode
    /// </summary>
    /// <param name="response">The response avaiable for the player in GUI that will make him reach this node.</param>
    /// <param name="message">The message displayed on GUI when this node is reached.</param>
    /// <param name="children">A list of TreeNodes reachable from this one.</param>
    /// <param name="rewardIDs">The list of item reward ID's the player is going to win.</param>
    public RewardTreeNode(string response, string message, List<DialogTreeNode> children, List<int> rewardIDs)
        : base(response, message, children)
    {
        this.rewardIDs = rewardIDs;
    }

    /// <summary>
    /// When the node is reached, gives a list of rewards for the player
    /// </summary>
    public override void Execute()
    {
        User user = User.Instance;

        foreach(int id in RewardIDs)
        {
            user.addItem(GameManager.Instance.itemManager.getItem(id));
        }
    }
}
