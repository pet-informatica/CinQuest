using UnityEngine;
using System.Collections;

public class Quest3NPCTeacher : MonoBehaviour, IBroadcaster {

	SceneChanger changer;

	void Start () {
		changer = GetComponent<SceneChanger> ();
	}
	

	void ChangeState(){
		if (GameStateMachine.Instance.Quest3Teacher == Quest3Teacher.WaitingPlayer) {
			changer.Change ();
			GameStateMachine.Instance.Quest3Teacher++;
		}
	}

	public void Broad(){
		ChangeState ();
	}
}
