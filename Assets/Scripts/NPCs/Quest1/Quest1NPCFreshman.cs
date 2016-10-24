using UnityEngine;
using System.Collections;

public class Quest1NPCFreshman : MonoBehaviour, IBroadcaster {

	public enum EState{
		AtGate,
		WaitingGatekeeper,
		AtHelpdesk,
		QuestEnd
	}
		
	public GameObject[] paths;
	InterativeSpeaker speaker;
	NPCMover mover;
	Move move;
	EState state;
	int currentDialog;
	int currentPath;

	void Start () {
		state = EState.AtGate;
		move = GetComponent<Move> ();
		mover = GetComponent < NPCMover> ();
		speaker = GetComponent<InterativeSpeaker> ();
	}

	void Update () {
		
	}

	/// <summary>
	/// Called by a broadcaster like the dialog tree node.
	/// </summary>
	public void Broad(){
		ChangeState ();
	}

	/// <summary>
	/// Moves to current target path in current state.
	/// </summary>
	void MoveToPath(){
		mover.GoForTargetWaypoint (paths [currentPath]);
	}

	/// <summary>
	/// Speaks the next dialog in the list latter when interacting with player.
	/// </summary>
	void NextDialog(){
		speaker.defaultDialogIndex++;
	}

	/// <summary>
	/// Advances the NPC to the next state in it's list, calling a method in the process.
	/// </summary>
	public void ChangeState(){
		if (state == EState.AtGate) {
			MoveToPath ();
			NextDialog ();
		}
		state++;
	}
}
