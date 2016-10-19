using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Developed by: Higor (hcmb)
/// A DialogTreeNode is used for building a DialogTree. Shows text conversations only. Can be extended by other
/// classes such as QuestTreeNode or RewardTreeNode in order to add additional logic to the process of a conversation,
/// such as changing the state of a quest or giving a reward for the player.
/// </summary>
[CreateAssetMenu]
[Serializable]
public class DialogTreeNode : ScriptableObject
{
	[SerializeField]
	List<int> preconditionIds;
	/// <summary>
	/// The list of the Precondition ID's that the user must satisfie in order to reach this node.
	/// </summary>
	public List<int> PreconditionIds
	{
		get { return preconditionIds; }
	}

	[SerializeField]
	List<int> rewardIds;
	/// <summary>
	/// The list of the reward item ID's that the user will win after reaching this node
	/// </summary>
	public List<int> RewardIds
	{
		get { return rewardIds; }
	}

    [SerializeField]
    string response;
    /// <summary>
    /// The response from the player wich will make him reach this tree node.
    /// </summary>
    public string Response
    {
        get { return response; }
    }

    [SerializeField]
    string message;
    /// <summary>
    /// The message that is displayed by this node in the GUI.
    /// </summary>
    public string Message
    {
        get { return message; }
    }

    [SerializeField]
    List<DialogTreeNode> children;
    /// <summary>
    /// A list of every reachable TreeNode node from the current one.
    /// </summary>
    public List<DialogTreeNode> Children
    {
        get { return children; }
    }

    /// <summary>
    /// Set to TRUE if there isn't any tree node reachable from the current one. Indicates the end of a dialog.
    /// </summary>
    public bool IsLeaf
    {
        get { return Children.Count == 0; }
    }

    /// <summary>
    /// The number of reachable and avaiable nodes from the current one.
    /// </summary>
    public int AvaiableChildren
    {
        get
        {
            int avaiable = 0;
            foreach(DialogTreeNode node in children)
            {
                if (node.IsAvaiable())
                {
                    avaiable++;
                }
            }
            return avaiable;
        }
    }

    /// <summary>
    /// Execute it when needed to traverse down the tree and go for a child node. Additional logic
    /// can be added by inherited classes overriding this method in order to execute an action in 
    /// the process of changing the current dialog tree node.
    /// </summary>
    /// <param name="response">The player response index for traversing the tree. If the index is
    /// pointing to an UnnavaiableNode, the method will go for the first AvaiableNode it finds. If
    /// there is node, it will return null.</param>
    /// <returns>The child wich was reached by the player response index.</returns>
    public virtual DialogTreeNode FetchAvaiable(int response)
    {
        int index = -1;
        while (response >= 0)
        {
            index++;
            if (index >= Children.Count)
                return null;

            if (Children[index].IsAvaiable())
                response--;
        }
        return Children[index];
    }

	/// <summary>
	/// Check if all the pre conditions are satisfied.
	/// </summary>
	/// <returns>TRUE if the current node can be reached. False otherwise.</returns>
	public virtual bool IsAvaiable()
	{
		User user = User.Instance;

		foreach(int id in preconditionIds)
		{
			if (!GameManager.Instance.preConditionManager.getPreCondition (id).checkIfMatches (user))
				return false;	
		}

		return true;
	}

	/// <summary>
	/// When the node is reached, gives a list of rewards for the player
	/// </summary>
	public virtual void Execute()
	{
		User user = User.Instance;

		foreach(int id in RewardIds)
		{
			user.AddItem(GameManager.Instance.itemManager.GetItem(id));
		}
	}
}