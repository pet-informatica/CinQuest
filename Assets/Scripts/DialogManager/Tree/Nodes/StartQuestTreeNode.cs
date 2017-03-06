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
public class StartQuestTreeNode : DialogTreeNode
{
    [SerializeField]
    int questId;
    /// <summary>
    /// The list of the reward item ID's that the user will win after reaching this node
    /// </summary>
    public int QuestID
    {
        get { return questId; }
    }


    /// <summary>
    /// Check if the current node is avaiable. It won't be if it is starting a quest that is locked, or alredy done.
    /// </summary>
    /// <returns>Returns if the current node is avaiable to be reached</returns>
    public override bool IsAvaiable()
    {
		if (!base.IsAvaiable ()) {
			return false;
		}
			
        User user = User.Instance;
        Quest quest = user.GetQuest(QuestID);
		quest.Activate (user);
        if (quest != null && quest.Unlocked && !quest.Done)
        {
			
            return true;
        }
        return false;
    }

    /// <summary>
    /// When the node is reached, gives a list of rewards for the player
    /// </summary>
    public override void Execute()
    {
		base.Execute ();
        User user = User.Instance;
        Quest quest = user.GetQuest(QuestID);
        if (quest != null && !quest.Unlocked)
        {
            quest.Activate(user);
            GameManager.Instance.UpdateQuestUI();
        }
    }
}
