using UnityEngine;
using System.Collections.Generic;

public class Quest1NPCGatekeeper : Gatekeeper, IBroadcaster {

	enum State{
		Fresh
	};

	State state;
	public  GameObject freshman;
	public  Transform freshmanDestine;

	/// <summary>
	/// Called by a broadcaster like the dialog tree node.
	/// </summary>
	public void Broad(){
		if (state == State.Fresh) {
			List<GameObject> teleport = new List<GameObject> ();
			teleport.Add (player);
			teleport.Add (freshman);
			List<Transform> destines = new List<Transform> ();
			destines.Add (origin);
			destines.Add (freshmanDestine);
			teleporter.Teleport (teleport, true, destines);
			freshman.GetComponent<Quest1NPCFreshman> ().ChangeState ();
			state++;
		} else {
			if (playerInside)
				teleporter.Teleport (player, true, destine);
			else
				teleporter.Teleport (player, true, origin);
		}
	}
}
