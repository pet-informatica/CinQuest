using UnityEngine;
using System.Collections;

/// <summary>
/// Quest1 NPC helpdesk situation.
/// </summary>
public class Quest1NPCHelpdesk : MonoBehaviour, IBroadcaster 
{
	/// <summary>
	/// Changes the state of NPC Helpdesk during the quest.
	/// </summary>
	void ChangeState()
	{
		if (GameStateMachine.Instance.Quest1Helpdesk == Quest1Helpdesk.WaitingPlayer) 
		{
			Quest1NPCFreshman freshman = GameObject.Find ("Quest1NPCFreshman").GetComponent<Quest1NPCFreshman> ();
			freshman.ChangeState ();
			SceneChanger.globalLock = false;
			NPCListener.Instance.Disable ("Quest1CinParkingFreshmenCCEN");
			NPCListener.Instance.Disable ("Quest1NPCFreshman");
		}
		GameStateMachine.Instance.Quest1Helpdesk++;
	}

	/// <summary>
	/// Called by a broadcaster like the dialog tree node.
	/// </summary>
	public void Broad()
	{
		ChangeState ();
	}
}
