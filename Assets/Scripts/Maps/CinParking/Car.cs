using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    /*
        Developed by: Higor

        Description: This is script is responsible for making cars move around in the CinParking scene.
        The car, once spawned by the CanSpawner class, can either keep going foward for it's deadEnd,
        were it's destroyed, or it can choose to park in the CinParking and stay there until it wants
        to unpark.

        How to use it: Attach it to a car prefab, along with a move script.
    */

    [HideInInspector]
    public CarSpawner spawner;
  
    public bool parked = false;

    Move move;
	
	void Start ()
    {
        move = GetComponent<Move>();
	}
	
	void Update ()
    {
        if(move.hasCompleted && !parked)
            move.moveToPoints();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "CarDeadEnd")
        {
            spawner.spawnedAmount--;
            Destroy(this.gameObject);
        }
    }
}
