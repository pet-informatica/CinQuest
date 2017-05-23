using UnityEngine;
using System.Collections;

/// <summary>
/// the waypoints are present in CinParking scene and are used for controlling the
///	spots were the randomly generated cars can or cannot park, as also where the wanderers can travel.
///
/// How to use it: Attach it to a gameobject with a transform and sets it's previous waypoint, that is,
/// the waypoint from where the car/wanderer is coming from before reaching this spot. Note that doing it for 
///	every waypoint, you will have a complete path for the car to travel and reach it.
/// 
/// Developed by: Higor
/// </summary>
public class CarWaypoint : Waypoint
{
    public CarSpawner carSpawner;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Car" && this.tag == "ParkingSpot")
        {
            collider.GetComponent<Car>().parked = true;
            collider.GetComponent<Car>().parkedAt = this.gameObject;
            collider.GetComponent<AudioSource>().enabled = false;
            collider.GetComponentInChildren<AudioSource>().enabled = false;
            carSpawner.parkingAmount--;
            carSpawner.parkedAmount++;
        }
           
    }
}
