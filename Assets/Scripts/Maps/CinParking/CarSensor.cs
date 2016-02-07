using UnityEngine;
using System.Collections;

public class CarSensor : MonoBehaviour
{
    public float breakTime = 5f;
    public float accelerationTime = 0.5f;

    Car car;
    Move move;
    Animator anim;
    Transform parent;
    float originalSpeed;
    bool stop;

	void Start ()
    {
        move = GetComponentInParent<Move>();
        anim = GetComponentInParent<Animator>();
        parent = GetComponentInParent<Transform>();
        car = GetComponentInParent<Car>();
        originalSpeed = move.moveSpeed;
	}

    void Update()
    {
        if(stop)
        {
            move.moveSpeed = Mathf.Lerp(move.moveSpeed, 0, breakTime * Time.deltaTime);
        }
        else
        {
            move.moveSpeed = Mathf.Lerp(move.moveSpeed, originalSpeed, accelerationTime * Time.deltaTime);
        }

        UpdateSensorPosition();
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
        if (col.tag == "Car" || col.tag == "Player")
            stop = true;
        else
            stop = false;
	}

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Car" || col.tag == "Player")
            stop = false;   
    }
}
