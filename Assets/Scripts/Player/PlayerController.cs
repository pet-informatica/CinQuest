using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    /*
        Developed By: Higor

        Description: It simply moves the player around according to W,A,S,D keys. Also, you are able to run pressing shift.
        How to use: Place it into the player gameobject and fill the speed variables.
    */

    [HideInInspector]
    public bool InDialog { get; set; }

    public float runSpeed = 10000f;
    public float walkSpeed = 5000f;

    Rigidbody2D rb;
    Animator anim;
    Dialog dialog;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
	}


    void Start()
    {
        InDialog = false;
        dialog = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Dialog>();
    }

	void Update ()
    {
        if (dialog.IsSpeaking) InDialog = true;
        else InDialog = false;

        float speed = walkSpeed;
        anim.SetBool("Running", false);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            anim.SetBool("Running", true);
        }

        if (InDialog)
        {
            speed = 0;
            anim.SetBool("Running", false);
        }

        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            direction.y = 1;
        else if (Input.GetKey(KeyCode.S))
            direction.y = -1;
        else if (Input.GetKey(KeyCode.A))
            direction.x = -1;
        else if (Input.GetKey(KeyCode.D))
            direction.x = 1;

        rb.velocity = direction * speed * Time.deltaTime;

        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetFloat("HorizontalSpeed", rb.velocity.x);
	}
}
