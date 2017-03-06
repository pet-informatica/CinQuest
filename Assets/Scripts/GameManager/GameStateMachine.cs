using UnityEngine;
using System.Collections.Generic;

public enum Quest1Freshman{
	AtGate,
	WaitingGatekeeper,
	WaitingPlayer,
	GoingForHelpdesk,
	AtHelpdesk,
	QuestEnd
} 

public enum Quest1GateKeeper{
	Fresh
}

public enum Quest1Helpdesk{
	WaitingPlayer,
	QuestEnd
}

public enum Quest1SecGrad{
	GivingLogin
}

public enum Quest5InfoGirl{
	WaitingPlayer
}

public enum Quest5Queue{
	WaitingPlayer
}

public enum Quest3Teacher{
	WaitingPlayer
}


/// <summary>
/// This class controls a huge list of states for all the npcs that acts based on it's current state in the game.
/// Since the states must survive between scenes, they are all stored here, in a singleton that isn't destroyed on load.
/// </summary>
public class GameStateMachine : MonoBehaviour 
{

	static GameStateMachine instance = null;
	/// <summary>
	/// The true instance for static accesing this class resources.
	/// </summary>
	public static GameStateMachine Instance
	{
		get { return instance; }
	}

	public Quest1Freshman Quest1Freshman { get ; set; }
	public Quest1GateKeeper Quest1GateKeeper { get ; set; }
	public Quest1Helpdesk Quest1Helpdesk { get ; set; }
	public Quest1SecGrad Quest1SecGrad { get ; set; }
	public Quest5InfoGirl Quest5InfoGirl { get; set; }
	public Quest5Queue Quest5Queue { get; set; }
	public Quest3Teacher Quest3Teacher { get; set; }


	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}
}
