using System;
using UnityEngine;

/// <summary>
/// Developed by: Peao (rngs);
/// NPC that represents the Cracha Giver from Staff. 
/// He should check some PreConditions before give his rewards, otherwise he will only speak normally.
/// </summary>
public class CrachaGiver : MonoBehaviour
{
	private const int _quest1Identifier = 1;
	private Quest _quest1;

	public CrachaGiver () {}

	void Start() {
		// TODO: Start some conversation 

		User currentUser = User.Instance;
		this.giveCracha(currentUser);
	}

	/// <summary>
	/// Checks if the quest1 is done.
	/// </summary>
	/// <returns><c>true</c>, if quest1 is done, <c>false</c> otherwise.</returns>
	/// <param name="currentUser">Current user.</param>
	private bool checkIfQuest1Done(User currentUser){

		currentUser.Quests.TryGetValue (_quest1Identifier, out _quest1);

		if (_quest1 != null) {
			return _quest1.done;
		} else
			return false;
		
	}
		
	/// <summary>
	/// Gives the cracha.
	/// </summary>
	/// <param name="currentUser">Current user.</param>
	private void giveCracha(User currentUser){
		if (checkIfQuest1Done (currentUser)) {
			foreach (GenericItem reward in _quest1.getRewards(currentUser)) {
				currentUser.addItem (reward);
			}
			//TODO: Starts a conversation about the Cracha
		} else {
			//TODO: Starts a random conversation, e.g. Could tell something about the way to redo the Cracha
		}
	}
}

