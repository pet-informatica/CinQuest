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

    public int parkingPercentage = 50;

    [HideInInspector]
    public Transform deadEnd;
    [HideInInspector]
    public CarSpawner spawner;

    Move move;
	
	void Start ()
    {
        move = GetComponent<Move>();

        int rand = Random.Range(0, 100);
        bool shouldPark = rand < parkingPercentage ? true : false;

        if(shouldPark)
        {
            GoForDeadEnd();
        }
        else
        {
            GoForDeadEnd();
        }
        
	}
	
	void Update ()
    {
        if(move.hasCompleted)
            move.moveToPoints();
	}

    void GoForDeadEnd()
    {
        move.addPoint(new Point(deadEnd.position.x, deadEnd.position.y, false));
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
