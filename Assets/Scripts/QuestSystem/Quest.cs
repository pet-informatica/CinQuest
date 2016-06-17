using System.Collections.Generic;

/// <summary>
/// Developed by: Peao (rngs);
/// Class that represents a Quest.
/// </summary>
public class Quest
{
    int identifier;
    /// <summary>
    /// The formal unique int identifier for indexing this quest.
    /// </summary>
    public int Identifier
    {
        get { return identifier; }
    }

    string name;
    /// <summary>
    /// The informal quest name for displaying in UI.
    /// </summary>
    public string Name
    {
        get { return name; }
    }

    string description;
    /// <summary>
    /// The informal quest description for displaying in UI.
    /// </summary>
    public string Description
    {
        get { return description; }
    }

    bool unlocked;
    /// <summary>
    /// True if this quest is avaible for the player to start. Occurs when his items satisfies all the preconditionsToUnlock.
    /// </summary>
    public bool Unlocked
    {
        get { return unlocked; }
    }

    bool done;
    /// <summary>
    /// True if this quest was alredy finished by the player. Occurs when his items satifies all the preconditionsToDone.
    /// </summary>
    public bool Done
    {
        get { return done; }
    }

    List<IPreCondition> preconditionsToUnlock;
    /// <summary>
    /// The preconditions required for this quest to become unlocked.
    /// </summary>
    public List<IPreCondition> PreconditionsToUnlock
    {
        get { return preconditionsToUnlock; }
    }

    List<IPreCondition> preconditionsToDone;
    /// <summary>
    /// The preconditions required for this quest be done.
    /// </summary>
    public List<IPreCondition> PreconditionsToDone
    {
        get { return preconditionsToDone; }
    }

	List<GenericItem> rewards;
    /// <summary>
    /// A list of GenericItem the player will get by finished this quest.
    /// </summary>
    public List<GenericItem> Rewards
    {
        get { return rewards; }
    }

	public Quest() {}

    /// <summary>
    /// The Quest constructor.
    /// </summary>
    /// <param name="identifier">The unique formal quest id int.</param>
    /// <param name="name">The informal quest title.</param>
    /// <param name="description">The informal quest description.</param>
    /// <param name="unlocked">If the quest is unlocked</param>
    /// <param name="preconditionsToUnlock">A list of IPrecondition the user must match in order to unlock this quest.</param>
    /// <param name="preconditionsToDone">A list of IPrecondition the user must match in order to finish this quest.</param>
    /// <param name="rewards">A list o GenericItem the user will be rewarded when finishing this quest.</param>
	public Quest (int identifier, string name, string description, bool unlocked, List<IPreCondition> preconditionsToUnlock, List<IPreCondition> preconditionsToDone, List<GenericItem> rewards)
	{
		this.identifier = identifier;
		this.name = name;
		this.description = description;
		this.unlocked = unlocked;
		this.done = false;
		this.preconditionsToUnlock = preconditionsToUnlock;
		this.preconditionsToDone = preconditionsToDone;
		this.rewards = rewards;
	}

    /// <summary>
    /// Check if the user satisfies all the preconditions to finish the quest, and return a list of rewards if it does.
    /// </summary>
    /// <param name="currentUserProfile">The user used for searching the preconditions.</param>
    /// <returns>The List<GenericItem> containing the rewards. NULL if the user doesn't satisfie the preconditions.</returns>
	public List<GenericItem> GetRewards(User currentUserProfile){
		if (this.CheckPreConditionsStatus (currentUserProfile, preconditionsToDone))
			return this.rewards;
		return null;
	}

	/// <summary>
	/// Tries to activate the Quest based on a user profile preconditions.
	/// </summary>
	/// <param name="currentUserProfile">Current user profile for checking the preconditions to unlock the quest.</param>
	public bool Activate(User currentUserProfile){
		if (this.CheckPreConditionsStatus (currentUserProfile, preconditionsToUnlock)) {
			this.unlocked = true;
			return true;
		}
		return false;
	}

    /// <summary>
    /// Tries to set the Quest to done based on currentUserProfile generic items.
    /// </summary>
    /// <param name="currentUserProfile">Current user profile for checking the preconditions.</param>
    /// <returns></returns>
    public bool Finish(User currentUserProfile)
    {
        if(this.CheckPreConditionsStatus(currentUserProfile, preconditionsToDone))
        {
            this.done = true;
            
            foreach (GenericItem item in rewards)
            {
                currentUserProfile.AddItem(item);
               
            }
            return true;
        }
        return false;
    }

    // <summary>
    /// Checks the pre conditions status.
    /// </summary>
    /// <returns><c>true</c>, if pre conditions status was checked, <c>false</c> otherwise.</returns>
    /// <param name=currentUserProfile">Current user profile.</param>
    /// <param name="preConditions">Pre conditions.</param>/
    private bool CheckPreConditionsStatus(User currentUserProfile, List<IPreCondition> preConditions){
		foreach (IPreCondition p in preConditions)
        {
			if (!p.checkIfMatches(currentUserProfile))
            {
                UnityEngine.Debug.Log("player got not ");
                return false;
            }
		}
		return true;
	}		
}