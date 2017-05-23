using UnityEngine;
using System.Collections;

/// <summary>
/// This is script is responsible for making cars move around in the CinParking scene.
///	The car, once spawned by the CanSpawner class, can either keep going foward for it's deadEnd,
///	were it's destroyed, or it can choose to park in the CinParking and stay there until it wants
///	to unpark. See CarSpawner class for more.
///
///	How to use it: Attach it to a car prefab, along with a move script.
/// 
/// Developed by: Higor
/// </summary>
public class Car : Mover
{
    [HideInInspector]
    public GameObject parkedAt;
  
    [HideInInspector]
    public bool parked = false;

    [HideInInspector]
    public bool unparking = false;

	/// <summary>
	/// Kills a car when it reached it's spawner dead end. Also, connects with it's spawner and unspawn itself.
	/// </summary>
	/// <param name="col">Col.</param>
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "DeadEnd")
        {
            spawner.spawnedAmount--;

            if (unparking)
            {
                if(spawner is CarSpawner)
                    ((CarSpawner)spawner).unparkingAmount--;
            }

            Destroy(this.gameObject);
        }
    }
}
