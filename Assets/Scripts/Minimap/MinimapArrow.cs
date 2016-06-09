using UnityEngine;
using System.Collections;

public class MinimapArrow : MonoBehaviour
{

    Animator anim;

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void OnLevelWasLoaded()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
            anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    

    void UpdateSensorPosition()
    {
        if (anim == null)
            return;

        if (anim.GetFloat("VerticalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 180);
        else if (anim.GetFloat("VerticalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (anim.GetFloat("HorizontalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 90);
        else if (anim.GetFloat("HorizontalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 270);
    }

    void Update()
    {
        UpdateSensorPosition();
    }
}
