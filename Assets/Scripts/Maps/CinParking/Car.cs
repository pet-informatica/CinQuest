using UnityEngine;
using System.Collections;

public class Car : Mover
{
    /*
        Developed by: Higor

        Description: This is script is responsible for making cars move around in the CinParking scene.
        The car, once spawned by the CanSpawner class, can either keep going foward for it's deadEnd,
        were it's destroyed, or it can choose to park in the CinParking and stay there until it wants
        to unpark. See CarSpawner class for more.

        How to use it: Attach it to a car prefab, along with a move script.
    */

    [HideInInspector]
    public GameObject parkedAt;
  
    [HideInInspector]
    public bool parked = false;

    [HideInInspector]
    public bool unparking = false;

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
