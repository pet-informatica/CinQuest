using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    /*
        Developed by: Higor

        Description: This scripts randomly generates cars and assing a path for it to move on.

        How to use it: Put it into a gameobject and place the gameobject where you want the cars to spawn.

    */

    public int maxCars = 15;
    public float spawnPerMinute = 10f;
    public float randomExtraSpawn = 5f;
    public float randomSpeedChanger = 50f;
    public int carsParkPercentage = 50;

    public List<GameObject> cars;
    public Transform deadEnd;
    float time;
    float spawn;

    [HideInInspector]
    public int spawnedAmount;
    
	void Start ()
    {
        spawn = spawnPerMinute + Random.Range(-randomExtraSpawn, randomExtraSpawn);
	}
	
	void Update ()
    {
        time += Time.deltaTime;

        if(time >= 60f / spawn && spawnedAmount <= maxCars)
            SpawnCar();
	}
    
    void SpawnCar()
    {
        int rand = Random.Range(0, cars.Count);
        GameObject car = Instantiate(cars[rand], transform.position, Quaternion.identity) as GameObject;

        car.GetComponent<Car>().spawner = this;

        Move move = car.GetComponent<Move>();
        move.moveSpeed += Random.Range(-randomSpeedChanger, randomSpeedChanger);

        rand = Random.Range(0, 100);
        bool shouldPark = rand < carsParkPercentage ? true : false;

        if(shouldPark)
        {
            GameObject[] parkingSpots = GameObject.FindGameObjectsWithTag("ParkingSpot");

            ParkingSpot[] spots = new ParkingSpot[parkingSpots.Length];

            for (int i = 0; i < parkingSpots.Length; i++)
                spots[i] = parkingSpots[i].GetComponent<ParkingSpot>();

            int foundAvaibleSpot = -1;
            for(int i = 0; i < spots.Length; i++)
            {
                if(spots[i].avaiable)
                {
                    foundAvaibleSpot = i;
                    spots[foundAvaibleSpot].avaiable = false;
                    break;
                }
            }

            if(foundAvaibleSpot >= 0)
            {
                List<Point> trip = new List<Point>();

                GameObject currentPoint = parkingSpots[foundAvaibleSpot];
                while(currentPoint != null)
                {
                    trip.Add(new Point(currentPoint.transform.position.x, currentPoint.transform.position.y, true));
                    currentPoint = currentPoint.GetComponent<ParkingSpot>().previous;
                }

                for(int i = trip.Count-1; i >= 0; i--)
                {
                    move.addPoint(trip[i]);
                }
            }
            else
            {
                GoForDeadEnd(move);
            }
        }
        else
        {
            GoForDeadEnd(move);
        }

        spawn = spawnPerMinute + Random.Range(-randomExtraSpawn, randomExtraSpawn);
        spawnedAmount++;
        time = 0;
    }

    void GoForDeadEnd(Move move)
    {
        move.addPoint(new Point(deadEnd.position.x, deadEnd.position.y, false));
    }
}
