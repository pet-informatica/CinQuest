using UnityEngine;
using System.Collections;

public class PC : MonoBehaviour {
	public float maxMove_speed = 10f;
	public Mover mover = new Mover();
	private Animator anim;
	private bool talking;

	private Camera pcCamera;
	void Start () {
		this.anim = GetComponent<Animator> ();
		this.talking = false;

		//this.mover.addPoint (new Point(this.transform.position.x + 100, transform.position.y),true);
		//this.mover.moveToPoints ();
		this.mover.maxMove_speed = this.maxMove_speed;
		//this.mover.setGameObject (this.gameObject, this.anim, this.maxMove_speed);
		//this.agent = GetComponent<NavMeshAgent>();
		//target = GameObject.FindWithTag("Player").transform; 
		this.pcCamera = GameObject.FindWithTag("MainCamera").camera;
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
		if(!this.mover.isMoving ()){
			if (!talking) {
				float moveVertical = 0, moveHorizontal = 0;
				this.setAnimation (moveHorizontal, moveVertical);
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
	}

	public void setTalking(bool talking){
		this.talking = talking;
	}

	void OnGUI(){
		GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),Resources.Load<Texture>("resultado.mov"));
	}

}
