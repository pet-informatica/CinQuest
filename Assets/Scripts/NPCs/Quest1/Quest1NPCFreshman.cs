using UnityEngine;
using System.Collections;

public class Quest1NPCFreshman : MonoBehaviour, IBroadcaster 
{
	public GameObject[] paths;
	InterativeSpeaker speaker;
	NPCMover mover;
	int currentDialog;
	int currentPath;

	void Start () 
	{
		mover = GetComponent < NPCMover> ();
		speaker = GetComponent<InterativeSpeaker> ();
	}

	void Update () 
	{
		
		if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.WaitingPlayer) 
		{
			if (speaker.Speak ()) 
			{
				ChangeState ();
			}
		} 
		else if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.GoingForHelpdesk) 
		{
			if (!mover.isMoving ()) 
			{
				ChangeState ();
			}
		}
	}

	/// <summary>
	/// Called by a broadcaster like the dialog tree node.
	/// </summary>
	public void Broad()
	{
		ChangeState ();
	}

	/// <summary>
	/// Moves to current target path in current state.
	/// </summary>
	void MoveToPath()
	{
		mover.GoForTargetWaypoint (paths [currentPath]);
		currentPath++;
	}

	/// <summary>
	/// Stops every movement
	/// </summary>
	void Halt()
	{
		mover.CancelPath ();
	}

	/// <summary>
	/// Speaks the next dialog in the list latter when interacting with player.
	/// </summary>
	void NextDialog()
	{
		speaker.defaultDialogIndex++;
	}

	/// <summary>
	/// Advances the NPC to the next state in it's list, calling a method in the process.
	/// </summary>
	public void ChangeState()
	{
		if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.AtGate) 
		{
			MoveToPath ();
			NextDialog ();
		} 
		else if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.WaitingGatekeeper)
		{
			Halt ();
			NextDialog ();
		} 
		else if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.WaitingPlayer) 
		{
			MoveToPath ();
		} 
		else if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.GoingForHelpdesk) 
		{
			NextDialog ();
		} 
		else if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.AtHelpdesk) 
		{
			NextDialog ();
		} 
		else if (GameStateMachine.Instance.Quest1Freshman == Quest1Freshman.QuestEnd)
		{
			MoveToPath ();
		}
		GameStateMachine.Instance.Quest1Freshman++;
	}
}
