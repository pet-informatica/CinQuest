using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSensor : MonoBehaviour
{
    /*
        Developed by: Higor

        Description: This script is responsible for taking care about the cars movement. When they move,
        this sensor check's if there is a player, or another car in front of it, and if true, it stops the car,
        avoiding a collision.

        How to use the script: Put it into a gameobject named sensor that is a child of the car. This gameobject
        must have a collider2d attach to it, and it must be in the right position in front of it. When the car
        change direction, the object is rotated, so the collider must be about 80 pixels away from the center pivot,
        so it will rotate accordingly.
    */

    public float breakTime = 5f;
    public float accelerationTime = 0.5f;
    public float hornsPerMinute = 10f;
    public List<AudioClip> horns;

    Move move;
    Animator anim;
    AudioSource source;
    float originalSpeed;
    float hornTime;
    bool stop;

    void Awake()
    {
        move = GetComponentInParent<Move>();
        anim = GetComponentInParent<Animator>();
        source = GetComponent<AudioSource>();
        originalSpeed = move.moveSpeed;
    }

    void Update()
    {
        hornTime += Time.deltaTime;

        if (stop)
        {
            move.moveSpeed = Mathf.Lerp(move.moveSpeed, -originalSpeed / 10f, breakTime * Time.deltaTime);

            if (hornTime > 60f / hornsPerMinute)
                PlayHorn();
        }
        else
        {
            move.moveSpeed = Mathf.Lerp(move.moveSpeed, originalSpeed, accelerationTime * Time.deltaTime);
        }

        UpdateSensorPosition();
    }

    void PlayHorn()
    {
        int r = Random.Range(0, horns.Count);
        source.PlayOneShot(horns[r]);
        hornTime = 0;
    }

    void UpdateSensorPosition()
    {
        if (anim.GetFloat("VerticalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (anim.GetFloat("VerticalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 180);
        else if (anim.GetFloat("HorizontalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 270);
        else if (anim.GetFloat("HorizontalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 90);
    }
	
	
	void OnTriggerStay2D (Collider2D col)
    {
        if (col.tag == "Car" || col.tag == "Player" || col.tag == "People")
            stop = true;
        else
            stop = false;
	}

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Car" || col.tag == "Player" || col.tag == "People")
            stop = false;   
    }
}
