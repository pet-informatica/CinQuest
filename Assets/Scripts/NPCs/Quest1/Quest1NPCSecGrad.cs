using UnityEngine;
using System.Collections;

/// <summary>
/// Quest1 NPC SecGrad reation.
/// </summary>
public class Quest1NPCSecGrad : MonoBehaviour, IBroadcaster 
{

	/// <summary>
	/// Called by a broadcaster like the dialog tree node.
	/// </summary>
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
