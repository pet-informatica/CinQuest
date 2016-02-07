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
        {
            int rand = Random.Range(0, cars.Count);
            GameObject car = Instantiate(cars[rand], transform.position, Quaternion.identity) as GameObject;
            car.GetComponent<Car>().deadEnd = deadEnd;
            car.GetComponent<Move>().moveSpeed += Random.Range(-randomSpeedChanger, randomSpeedChanger);
            car.GetComponent<Car>().spawner = this;
            spawn = spawnPerMinute + Random.Range(-randomExtraSpawn, randomExtraSpawn);
            spawnedAmount++;
            time = 0;
        }
	}
}
