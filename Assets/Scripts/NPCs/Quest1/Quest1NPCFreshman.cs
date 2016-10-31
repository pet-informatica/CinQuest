using UnityEngine;
using System.Collections;

public class Quest1NPCFreshman : MonoBehaviour, IBroadcaster {

	public enum EState{
		AtGate,
		WaitingGatekeeper,
		WaitingPlayer,
		GoingForHelpdesk,
		AtHelpdesk,
		QuestEnd
	}
		
	public GameObject[] paths;
	InterativeSpeaker speaker;
	NPCMover mover;
	EState state;
	int currentDialog;
	int currentPath;

	void Start () {
		state = EState.AtGate;
		mover = GetComponent < NPCMover> ();
		speaker = GetComponent<InterativeSpeaker> ();
	}

	void Update () {
		if (state == EState.WaitingPlayer) {
			if (speaker.Speak ()) {
				ChangeState ();
			}
				//ChangeState ();
		} else if (state == EState.GoingForHelpdesk) {
			if (!mover.isMoving ()) {
				ChangeState ();
			}
		}
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
		currentPath++;
	}

	/// <summary>
	/// Stops every movement
	/// </summary>
	void Halt(){
		mover.CancelPath ();
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
		} else if (state == EState.WaitingGatekeeper) {
			Halt ();
			NextDialog ();
		} else if (state == EState.WaitingPlayer) {
			MoveToPath ();
		} else if (state == EState.GoingForHelpdesk) {
			NextDialog ();
		}
		state++;
	}
}
