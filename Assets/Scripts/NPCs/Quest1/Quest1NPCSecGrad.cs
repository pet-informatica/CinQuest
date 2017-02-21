using UnityEngine;
using System.Collections;

public class Quest1NPCSecGrad : MonoBehaviour, IBroadcaster 
{
	public void Broad()
	{
		if (GameStateMachine.Instance.Quest1SecGrad == Quest1SecGrad.GivingLogin) 
		{
			NPCListener.Instance.Disable ("Quest1CinParkingFreshmen");
			NPCListener.Instance.Disable ("Quest1CCENFreshmen");
			GameStateMachine.Instance.Quest1SecGrad++;
		}
	}
}
