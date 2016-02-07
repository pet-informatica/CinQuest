using UnityEngine;
using System.Collections;

public class ParkingSpot : MonoBehaviour
{
    /*
        Developed by: Higor

        Description: the ParkingSpots are present in CinParking scene and are used for controlling the
        spots were the randomly generated cars can or cannot park.

        How to use it: Attach it to a gameobject with a transform and sets it's previous spot, that is,
        the waypoint from where the car is coming from before reaching this spot. Note that doing it for 
        every waypoint, you will have a complete path for the car to travel and reach the spot.
    */

    public bool avaiable = true;
    public GameObject previous;
    public CarSpawner carSpawner;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Car" && this.tag == "ParkingSpot")
        {
            collider.GetComponent<Car>().parked = true;
            collider.GetComponent<Car>().parkedAt = this.gameObject;
            carSpawner.parkingAmount--;
            carSpawner.parkedAmount++;
        }
           
    }
}
