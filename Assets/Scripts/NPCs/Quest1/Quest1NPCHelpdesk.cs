using UnityEngine;
using System.Collections;

public class Quest1NPCHelpdesk : MonoBehaviour, IBroadcaster 
{
	void ChangeState()
	{
		if (GameStateMachine.Instance.Quest1Helpdesk == Quest1Helpdesk.WaitingPlayer) 
		{
			Quest1NPCFreshman freshman = GameObject.Find ("Quest1NPCFreshman").GetComponent<Quest1NPCFreshman> ();
			freshman.ChangeState ();
			NPCListener.Instance.DisableInstantly ("Quest1CinExitBlock");
			NPCListener.Instance.Disable ("Quest1CinParkingFreshmenCCEN");
			NPCListener.Instance.Disable ("Quest1NPCFreshman");
		}
		GameStateMachine.Instance.Quest1Helpdesk++;
	}

	public void Broad()
	{
		ChangeState ();
	}
}
