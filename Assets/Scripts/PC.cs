using UnityEngine;
using System.Collections;

public class PC : MonoBehaviour {
	public float maxMove_speed = 10f;
	private Animator anim;
	private bool talking;
	private PathPlanner pathPlanner;

	private Camera pcCamera;
	void Start () {
		this.anim = GetComponent<Animator> ();
		this.talking = false;
		//this.agent = GetComponent<NavMeshAgent>();
		//target = GameObject.FindWithTag("Player").transform; 
		this.pcCamera = GameObject.FindWithTag("MainCamera").camera;

		this.pathPlanner = new PathPlanner ();

	}




	void setAnimation(float moveHorizontal, float moveVertical)
	{
		this.anim.SetFloat ("VerticalSpeed",moveVertical);
		this.anim.SetFloat ("HorizontalSpeed", moveHorizontal);

		if( moveHorizontal > 0 ) {
			this.anim.Play("MovingRight",-1,1);
			//this.anim.Play("IdleRight",-1,Time.deltaTime);
		}
		else if( moveHorizontal < 0 ){
			this.anim.Play("MovingLeft",-1,0.5f);
			//this.anim.Play("IdleLeft",-1,Time.deltaTime);
		}
		else if( moveVertical < 0 ){
			this.anim.Play("MovingDown",-1,0.5f);
			//this.anim.Play("IdleDown",-1,Time.deltaTime);
		}
		else if( moveVertical > 0 ) {
			this.anim.Play("MovingUp",-1,0.5f);
			//this.anim.Play("IdleDown",-1,Time.deltaTime);
		}

		rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveVertical * this.maxMove_speed);
		rigidbody2D.velocity = new Vector2 (moveHorizontal * this.maxMove_speed, rigidbody2D.velocity.y);
	}




	// Update is called once per frame
	void FixedUpdate (){

	
		if(Input.GetMouseButtonDown(0))
		{

			Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

			this.pathPlanner.addGoal(this.pcCamera.ScreenToWorldPoint (mousePosition));
		
		}
		float moveVertical = 0, moveHorizontal = 0;
		this.transform.position = this.pathPlanner.processTrajectory (this.transform.position, out moveHorizontal, out moveVertical);
		this.setAnimation (moveHorizontal, moveVertical);
		//Debug.Log("MousePos: " + goalPosition + "CharPos: " + transform.position );

		if (!talking) {
			moveVertical = Input.GetAxis("Vertical");
			moveHorizontal = Input.GetAxis ("Horizontal");


			//this.agent.SetDestination(new Vector2(10,10));
			//(Input.mousePosition);
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
