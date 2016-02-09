using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSpawner : MoverSpawner
{
    /*
        Developed by: Higor

        Description: This scripts randomly generates cars and assing a path for it to move on. The newly generated car
        can either choose to PARK, in this case, gowing for a waypoint tagged as parking spot and staying there until
        the CarSpawner chooses to UNPARK it, or it can simply rush fowards to a waypoint tagged as deadend, where it will
        be out of the screen and destroyed.

        How to use it: Put it into a gameobject and place the gameobject where you want the cars to spawn. In order for that
        to work, you need to build a complete parking architecture, where you must have:

        1. Waypoints tagged as ParkingSpot, those are the spots where car's can park.
        2. Waypoints tagged as Waypoint, the points where the cars can travel before reaching a ParkingSpot.
        3. At least one Waypoint tagged as DeadEnd, the one place the car will be destroyed, if it choses so.
        4. You must connect every Previous variable from the waypoints in order to created a valid PATH for the
        car to travel through.

    */

    public int maxCars = 15;
    public float carSpeed = 300f;
    public bool carsCanPark = false;
    public int carsParkPercentage = 50;
    public float carUnparkTimeInSeconds = 120;

    float unparkTime;
    float unpark;

    [HideInInspector]
    public int parkedAmount;

    [HideInInspector]
    public int parkingAmount;

    [HideInInspector]
    public int unparkingAmount;
    
	protected override void Start ()
    {
        spawn = spawnEverySeconds + Random.Range(-spawnEverySeconds / 2f, spawnEverySeconds / 2f);
        unpark = carUnparkTimeInSeconds + Random.Range(-carUnparkTimeInSeconds / 2f, carUnparkTimeInSeconds / 2f);
    }

    protected override void Update ()
    {
        spawnTime += Time.deltaTime;

        if(spawnTime >= 60f / spawn && spawnedAmount <= maxCars)
            Spawn();

        unparkTime += Time.deltaTime;

        if(ShouldUnpark() && parkingAmount <= 0 && carsCanPark)
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
            if(carScripts[i] != null)
            {
                if (carScripts[i].parked)
                {
                    carToUnpark = i;
                    break;
                }
            } 
        }

        if(carToUnpark >= 0)
        {
            Move move = cars[carToUnpark].GetComponent<Move>();

            GameObject currentWaypoint = carScripts[carToUnpark].parkedAt.GetComponent<Waypoint>().previous;

            while(currentWaypoint != null)
            {
                move.addPoint(new Point(currentWaypoint.transform.position.x, currentWaypoint.transform.position.y, true));
                currentWaypoint = currentWaypoint.GetComponent<Waypoint>().previous;
            }

            move.addPoint(new Point(targetWaypoint.transform.position.x, targetWaypoint.transform.position.y, true));

            carScripts[carToUnpark].unparking = true;
            carScripts[carToUnpark].parked = false;
            cars[carToUnpark].GetComponent<AudioSource>().enabled = true;
            cars[carToUnpark].GetComponentInChildren<AudioSource>().enabled = true;
            parkedAmount--;
            unparkingAmount++;
            unpark = carUnparkTimeInSeconds + Random.Range(-carUnparkTimeInSeconds / 2f, carUnparkTimeInSeconds / 2f);
            unparkTime = 0;
        }
    }
    
    protected override void Spawn()
    {
        int rand = Random.Range(0, objects.Count);
        GameObject car = Instantiate(objects[rand], transform.position, Quaternion.identity) as GameObject;
        car.transform.parent = this.transform;

        Car c = car.GetComponent<Car>();
        c.spawner = this;
        
        Move move = car.GetComponent<Move>();
        move.moveSpeed = carSpeed + Random.Range(-carSpeed / 2f, carSpeed / 2f);

        rand = Random.Range(0, 100);
        bool shouldPark = rand < carsParkPercentage ? true : false;

        if(shouldPark && (!ShouldUnpark() || parkedAmount <= 0) && unparkingAmount <= 0 && carsCanPark)
        {
            GoForParking(move);
        }
        else
        {
            GoForTargetWaypoint(move);
        }

        spawn = spawnEverySeconds + Random.Range(-spawnEverySeconds / 2f, spawnEverySeconds / 2f);
        spawnedAmount++;
        spawnTime = 0;
    }

    void GoForParking(Move move)
    {
        GameObject[] parkingSpots = GameObject.FindGameObjectsWithTag("ParkingSpot");

        Waypoint[] spots = new Waypoint[parkingSpots.Length];

        for (int i = 0; i < parkingSpots.Length; i++)
            spots[i] = parkingSpots[i].GetComponent<Waypoint>();

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
                currentPoint = currentPoint.GetComponent<Waypoint>().previous;
            }

            for (int i = trip.Count - 1; i >= 0; i--)
            {
                move.addPoint(trip[i]);
            }

            move.moveSpeed = move.moveSpeed * 3f / 4f;
            parkingAmount++;
        }
        else
        {
            GoForTargetWaypoint(move);
        }
    }
}
