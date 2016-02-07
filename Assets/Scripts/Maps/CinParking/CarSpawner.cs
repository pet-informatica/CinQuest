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
    public bool carsCanPark = false;
    public int carsParkPercentage = 50;
    public float carUnparkTimeInSeconds = 120;
    public float carUnparkTimeRandom = 60;

    public List<GameObject> cars;
    public Transform deadEnd;
    float spawnTime;
    float unparkTime;
    float spawn;
    float unpark;

    [HideInInspector]
    public int spawnedAmount;

    [HideInInspector]
    public int parkedAmount;

    [HideInInspector]
    public int parkingAmount;

    [HideInInspector]
    public int unparkingAmount;
    
	void Start ()
    {
        spawn = spawnPerMinute + Random.Range(-randomExtraSpawn, randomExtraSpawn);
        unpark = carUnparkTimeInSeconds + Random.Range(-carUnparkTimeRandom, carUnparkTimeRandom);
    }
	
	void Update ()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= 60f / spawn && spawnedAmount <= maxCars)
            SpawnCar();

        unparkTime += Time.deltaTime;

        if( ShouldUnpark() && parkingAmount <= 0 && carsCanPark)
            UnparkCar();
	}

    bool ShouldUnpark()
    {
        return unparkTime >= unpark;
    }

    void UnparkCar()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        Car[] carScripts = new Car[cars.Length];

        for (int i = 0; i < cars.Length; i++)
            carScripts[i] = cars[i].GetComponent<Car>();

        int carToUnpark = -1;
        for(int i = 0; i < carScripts.Length; i++)
        {
            if (carScripts[i].parked)
            {
                carToUnpark = i;
                break;
            }
        }

        if(carToUnpark >= 0)
        {
            Move move = cars[carToUnpark].GetComponent<Move>();

            GameObject currentWaypoint = carScripts[carToUnpark].parkedAt.GetComponent<ParkingSpot>().previous;

            while(currentWaypoint != null)
            {
                move.addPoint(new Point(currentWaypoint.transform.position.x, currentWaypoint.transform.position.y, true));
                currentWaypoint = currentWaypoint.GetComponent<ParkingSpot>().previous;
            }

            move.addPoint(new Point(deadEnd.position.x, deadEnd.position.y, true));

            parkedAmount--;
            unparkingAmount++;
            unpark = carUnparkTimeInSeconds + Random.Range(-carUnparkTimeRandom, carUnparkTimeRandom);
            unparkTime = 0;
        }
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

        if(shouldPark && (!ShouldUnpark() || parkedAmount <= 0) && unparkingAmount <= 0 && carsCanPark)
        {
            GoForParking(move);
        }
        else
        {
            GoForDeadEnd(move);
        }

        spawn = spawnPerMinute + Random.Range(-randomExtraSpawn, randomExtraSpawn);
        spawnedAmount++;
        spawnTime = 0;
    }

    void GoForParking(Move move)
    {
        GameObject[] parkingSpots = GameObject.FindGameObjectsWithTag("ParkingSpot");

        ParkingSpot[] spots = new ParkingSpot[parkingSpots.Length];

        for (int i = 0; i < parkingSpots.Length; i++)
            spots[i] = parkingSpots[i].GetComponent<ParkingSpot>();

        int foundAvaibleSpot = -1;
        for (int i = 0; i < spots.Length; i++)
        {
            if (spots[i].avaiable)
            {
                foundAvaibleSpot = i;
                spots[foundAvaibleSpot].avaiable = false;
                break;
            }
        }

        if (foundAvaibleSpot >= 0)
        {
            List<Point> trip = new List<Point>();

            GameObject currentPoint = parkingSpots[foundAvaibleSpot];
            while (currentPoint != null)
            {
                trip.Add(new Point(currentPoint.transform.position.x, currentPoint.transform.position.y, true));
                currentPoint = currentPoint.GetComponent<ParkingSpot>().previous;
            }

            for (int i = trip.Count - 1; i >= 0; i--)
            {
                move.addPoint(trip[i]);
            }

            parkingAmount++;
        }
        else
        {
            GoForDeadEnd(move);
        }
    }

    void GoForDeadEnd(Move move)
    {
        move.addPoint(new Point(deadEnd.position.x, deadEnd.position.y, false));
    }
}
