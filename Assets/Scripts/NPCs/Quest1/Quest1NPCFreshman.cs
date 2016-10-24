using UnityEngine;
using System.Collections;

public class Quest1NPCFreshman : MonoBehaviour, IBroadcaster {

	public enum EState{
		AtGate,
		WaitingGatekeeper,
		AtHelpdesk,
		QuestEnd
	}
		
	public Waypoint pathToHelpdesk;
	InterativeSpeaker speaker;
	Move move;
	EState state;
	int currentDialog;

	void Start () {
		state = EState.AtGate;
		move = GetComponent<Move> ();
		speaker = GetComponent<InterativeSpeaker> ();
	}

	void Update () {
		
	}

	public void Broad(){
		ChangeState ();
	}

	public void ChangeState(){
		state++;
		speaker.defaultDialogIndex++;
	}
}
