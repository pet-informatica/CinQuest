using UnityEngine;
using System.Collections;

public class Quest5NPCQueue : MonoBehaviour, IBroadcaster {

	SceneChanger changer;

	void Start(){
		changer = GetComponent<SceneChanger> ();
	}
	
	void ChangeState(){
		if (GameStateMachine.Instance.Quest5Queue == Quest5Queue.WaitingPlayer) {
			NPCListener.Instance.Disable ("Quest5QueueNPCs");
			changer.Change ();
			GameStateMachine.Instance.Quest5Queue++;
		}
	}

	public void Broad(){
		ChangeState ();
	}
}
