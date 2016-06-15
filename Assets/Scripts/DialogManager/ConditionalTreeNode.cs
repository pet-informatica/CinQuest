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
    /// Constructs a new TreeNode
    /// </summary>
    /// <param name="response">The response avaiable for the player in GUI that will make him reach this node.</param>
    /// <param name="message">The message displayed on GUI when this node is reached.</param>
    /// <param name="children">A list of TreeNodes reachable from this one.</param>
    public ConditionalTreeNode(string response, string message, List<DialogTreeNode> children, List<int> preconditionIDs)
        :base(response, message, children)
    {
        this.preconditionIDs = preconditionIDs;
    }

    /// <summary>
    /// Check if all the pre conditions are satisfied.
    /// </summary>
    /// <returns>TRUE if the current node can be reached. False otherwise.</returns>
    public override bool IsAvaiable()
    {
        List<IPreCondition> preconditions = new List<IPreCondition>();
        if (preconditions.Count == 0)
        {
            foreach(int id in PreconditionIDs)
            {
                preconditions.Add(GameManager.Instance.preConditionManager.getPreCondition(id));
            }
        }

        User user = User.Instance;
        
        foreach(IPreCondition precondition in preconditions)
        {
            if (!precondition.checkIfMatches(user))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Returns an child node when indexed by a player response.
    /// </summary>
    /// <param name="response">The player response index for traversing the tree. If the index is
    /// pointing to an UnnavaiableNode, the method will go for the first AvaiableNode it finds. If
    /// there is node, it will return null.</param>
    /// <returns>The child wich was reached by the player response index.</returns>
    public override DialogTreeNode GoToChild(int response)
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

}
