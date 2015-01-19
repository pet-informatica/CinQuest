using UnityEngine;
using System.Collections;


public class INACIA_NPC : MonoBehaviour {
	public string [] falas;
	public GUISkin skin;
	public Texture image;
	private bool talking;
	private int lineCounter;
	private GameObject talkingTo;
	private bool answer;
	private bool complete;
	public Mover mover = new Mover();


	void Start () {
		this.complete = false;
		this.answer = true;
		this.talking = false;
		this.lineCounter = -1;
		this.mover.addPoint (new Point(this.transform.position.x + 200, transform.position.y + 120),true);
		//this.mover.moveToPoints ();
	}



	void FixedUpdate(){


		//Example for following someone
		if(talking)
		{
			if( Vector3.Distance(this.talkingTo.transform.position, this.transform.position) > 100 )
			{

				float moveHorizontal = 0, moveVertical = 0;

			}



		}

	}

	void Update(){

		


		//this.agent.SetDestination(new Vector3(10,10,0));
		if(this.lineCounter == 0 && talking){
			if(Input.GetAxis("Horizontal") < 0){
				this.answer = true;
			}else if(Input.GetAxis("Horizontal") > 0){
				this.answer = false;
			}
		}
		if (this.lineCounter >= this.falas.Length) {
			this.lineCounter = -1;
			this.talking = false;
			talkingTo.GetComponent<PC>().setTalking(this.talking);
			talkingTo.GetComponent<PC>().mover.addPoint( new Point(talkingTo.transform.position.x + 100, talkingTo.transform.position.y),true);
			talkingTo.GetComponent<PC>().mover.moveToPoints();
		}
		if (talking && Input.GetButtonDown ("Fire1")) {
			talkingTo.GetComponent<PC>().setTalking(this.talking);
			if (this.lineCounter == -1 && complete) {
				this.lineCounter = 3;
			}
			else if(answer && this.lineCounter == 0 && !complete){
				this.lineCounter = 1;
				this.complete = true;
				InventoryController iv = talkingTo.GetComponent<InventoryController>();
				iv.AddItem(new Item("Cracha",1,"Este eh o seu cracha de acesso ao CIn. Com ele voce podera entrar quando desejar.",Item.ItemType.Quest));
			}else if(!answer && this.lineCounter == 0 && !complete){
				this.lineCounter = 2;
			}else{
				if(this.lineCounter == 2) this.lineCounter = 5;
				else this.lineCounter++;
			}
		}
	}

	void OnGUI(){
		GUI.skin = this.skin;
		if (talking && this.lineCounter < this.falas.Length && this.lineCounter >= 0) {
			Vector3 position = Camera.main.ViewportToScreenPoint (new Vector3(0f,0.75f,0));
			GUI.BeginGroup(new Rect(position.x,position.y,Camera.main.pixelWidth,Camera.main.pixelHeight/4));
			Rect iconRec = new Rect(0f,1f,Camera.main.pixelWidth/6,Camera.main.pixelHeight/4);
			Rect msgRec = new Rect(Camera.main.pixelWidth/6,1f,Camera.main.pixelWidth-Camera.main.pixelWidth/6,Camera.main.pixelHeight/4);
			GUI.Box(iconRec,this.image);
			GUI.Box(msgRec,this.falas[this.lineCounter],"TextArea");
			GUI.EndGroup ();
			if (this.lineCounter == 0) {
				drawButton();
			}
		}
	}

	private void drawButton(){
		GUI.Button(new Rect(Camera.main.pixelWidth/2-50,Camera.main.pixelHeight/2,50,50),"Sim");
		GUI.Button(new Rect(Camera.main.pixelWidth/2+50,Camera.main.pixelHeight/2,50,50),"Nao");
		if (answer) {
			GUI.DrawTexture(new Rect(Camera.main.pixelWidth/2-50,Camera.main.pixelHeight/2-50,50,50), Resources.Load<Texture2D> ("Icons/pointer"));
		}else{
			GUI.DrawTexture(new Rect(Camera.main.pixelWidth/2+50,Camera.main.pixelHeight/2-50,50,50), Resources.Load<Texture2D> ("Icons/pointer"));
		}

	}

	void OnCollisionEnter2D(Collision2D objeto){
		this.talking = true;
		this.lineCounter = -1;
		this.talkingTo = objeto.gameObject;
	}

	void OnCollisionStay2D(Collision2D objeto){
		this.talking = true;


	}
}
