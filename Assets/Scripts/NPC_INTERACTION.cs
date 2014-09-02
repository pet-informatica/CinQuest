using UnityEngine;
using System.Collections;

public class NPC_INTERACTION : MonoBehaviour {
	public string [] falas;
	public Item recompensa;
	public GUISkin skin;
	public Camera camera = new Camera();
	public Texture image;
	private bool talking = false;
	private int lineCounter = 0;
	private GameObject talkingTo;
	// Use this for initialization
	void Start () {
		this.recompensa = new Item("gun",2,"Arma super segura e todos adoram!",Item.ItemType.Weapon);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (talking && Input.GetButtonDown ("Fire1")) {
			this.lineCounter++;
			if (this.lineCounter == this.falas.Length - 1) {
				InventoryController iv = talkingTo.GetComponent<InventoryController>();
				int posicao = iv.BuscarItem("grape");
				if(posicao > -1){
					iv.RemoveItem(posicao);
					iv.AddItem(this.recompensa);
				}
			}
		}
						
	}

	void OnGUI(){
		GUI.skin = this.skin;
		if (talking && this.lineCounter < this.falas.Length) {
			Vector3 position = this.camera.ViewportToScreenPoint (new Vector3(0f,0.75f,0));
			GUI.BeginGroup(new Rect(position.x,position.y,this.camera.pixelWidth,this.camera.pixelHeight/4));
			GUI.Box(new Rect(0f,1f,this.camera.pixelWidth/6,this.camera.pixelHeight/4),this.image);
			GUI.Box(new Rect(this.camera.pixelWidth/6,1f,this.camera.pixelWidth-this.camera.pixelWidth/6,this.camera.pixelHeight/4),this.falas[this.lineCounter],"TextArea");
			GUI.EndGroup ();
		}
	}

	void OnCollisionStay2D(Collision2D objeto){
		if (Input.GetButtonDown ("Fire1")) {
			talking = true;
			this.talkingTo = objeto.gameObject;
		}
		if (lineCounter >= this.falas.Length) {
			this.lineCounter = 0;
			this.talking = false;		
		}
	}

	void OnCollisionExit2D(Collision2D objeto){
		talking = false;
		this.lineCounter = 0;
	}
}
