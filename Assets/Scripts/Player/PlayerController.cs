using UnityEngine;
using System.Collections;

/// <summary>
/// Developed by: Higor (hcmb)
/// This class is responsible for moving the player and controlling it's animator
/// </summary>
public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public bool InDialog { get; set; }

    public float runSpeed = 10000f;
    public float walkSpeed = 5000f;

	private bool paused;

    Rigidbody2D rb;
    Animator anim;
    DialogManager dialog;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

    void Start()
    {
        InDialog = false;
        dialog = GameObject.FindGameObjectWithTag("DialogBox").GetComponent<DialogManager>();
    }

	/// <summary>
	/// Check if the player is on a dialog and set player speed to walk velocity.
	/// Check if the run button is held down to set the run speed.
	/// If the player is on a dialog or the game is pause, set the speed to 0.
	/// Check if the player is held down any moving button.
	/// Calculates the player velocity base on the information obtained in the checks
	/// </summary>
	void Update ()
    {
        if (dialog.IsSpeaking) InDialog = true;
        else InDialog = false;

        float speed = walkSpeed;
        anim.SetBool("Running", false);

        if (Input.GetButton("Run"))
        {
            speed = runSpeed;
            anim.SetBool("Running", true);
        }

        if (InDialog)
        {
            speed = 0;
            anim.SetBool("Running", false);
        }

		if (paused)
		{
			speed = 0;
			anim.SetBool("Running", false);
		}

        Vector2 direction = Vector2.zero;

        if (Input.GetButton("Up"))
            direction.y = 1;
        else if (Input.GetButton("Down"))
            direction.y = -1;
        else if (Input.GetButton("Left"))
            direction.x = -1;
        else if (Input.GetButton("Right"))
            direction.x = 1;

        rb.velocity = direction * speed * Time.deltaTime;

        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetFloat("HorizontalSpeed", rb.velocity.x);
	}

	public void OnPause(bool p) {
		this.paused = p;
	}
}
