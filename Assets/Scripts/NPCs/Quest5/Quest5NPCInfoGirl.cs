using UnityEngine;
using System.Collections;

public class Quest5NPCInfoGirl : MonoBehaviour, IBroadcaster {

	NPCMover mover;
	public GameObject[] paths;
	int cp;

	void Start () {
		mover = GetComponent<NPCMover> ();
		cp = 0;
	}

	void ChangeState(){
		if (GameStateMachine.Instance.Quest5InfoGirl == Quest5InfoGirl.WaitingPlayer) {
			mover.GoForTargetWaypoint (paths [cp++]);
			NPCListener.Instance.Disable ("Quest5NPCInfoGirl");
			GameStateMachine.Instance.Quest5InfoGirl++;
		}
	}

	public void Broad(){
		ChangeState ();
	}
}
