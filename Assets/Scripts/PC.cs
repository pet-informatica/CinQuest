using UnityEngine;
using System.Collections;

public class PC : MonoBehaviour {
	public float maxMove_speed = 10f;
	private Animator anim;
	private bool talking;
	// Use this for initialization
	void Start () {
		this.anim = GetComponent<Animator> ();
		this.talking = false;
	}
	
	// Update is called once per frame
	void FixedUpdate (){

		if (!talking) {
			float moveVertical = Input.GetAxis("Vertical");
			float moveHorizontal = Input.GetAxis ("Horizontal");
			if (Mathf.Abs (moveVertical) > Mathf.Abs (moveHorizontal))
				moveHorizontal = 0;
			else if (Mathf.Abs (moveHorizontal) > Mathf.Abs (moveVertical))
				moveVertical = 0;
			else {
				moveVertical = 0;
				moveHorizontal = 0;
			}
			this.anim.SetFloat ("VerticalSpeed",moveVertical);
			this.anim.SetFloat ("HorizontalSpeed", moveHorizontal);
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveVertical * this.maxMove_speed);
			rigidbody2D.velocity = new Vector2 (moveHorizontal * this.maxMove_speed, rigidbody2D.velocity.y);
		}else{
			rigidbody2D.velocity = new Vector2(0,0);
			this.anim.SetFloat ("VerticalSpeed",0);
			this.anim.SetFloat ("HorizontalSpeed", 0);
		}
	}

	public void setTalking(bool talking){
		this.talking = talking;
	}

}
