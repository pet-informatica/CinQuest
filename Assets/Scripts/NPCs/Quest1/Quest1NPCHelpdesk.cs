using UnityEngine;
using System.Collections;

public class Quest1NPCHelpdesk : MonoBehaviour, IBroadcaster 
{
	enum EState{
		WaitingPlayer,
		QuestEnd
	}

	EState state;

	void ChangeState(){
		if (state == EState.WaitingPlayer) {
			Quest1NPCFreshman freshman = GameObject.Find ("Quest1NPCFreshman").GetComponent<Quest1NPCFreshman> ();
			freshman.ChangeState ();
			NPCListener.Instance.Disable ("Quest1Freshmen");
		}
		state++;
	}

	public void Broad(){
		ChangeState ();
	}
}
