using UnityEngine;
using System.Collections;

public class Quest1NPCSecGrad : MonoBehaviour, IBroadcaster {

	enum State{
		GivingLogin
	};

	State state;

	public void Broad(){
		if (state == State.GivingLogin) {
			NPCListener.Instance.Disable ("Quest1CinParkingFreshmen");
			NPCListener.Instance.Disable ("Quest1CCENFreshmen");
			state++;
		}
	}
}
