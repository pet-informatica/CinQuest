using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

	public string[] falas;

	private int estado;
	private GuiFalaController falaController;
	private GameObject talkingTo;
	private bool talking;
	private int optionSelected;


	private const int IDLE = 0;

	// Use this for initialization
	void Start () {
		if (falaController == null) {
			falaController = gameObject.GetComponent<GuiFalaController> ();
		}
		estado = IDLE;
	}
	
	// Update is called once per frame
	void Update () {
		if (falaController.canExecuteCommand ()) {		
			if (estado == 1 && Input.GetKeyDown (KeyCode.Space)) {
					falaController.showDialog ("primeira mensagem!");
					estado = 2;
                this.talkingTo.GetComponent<PlayerController>().CanMove = false;
			} else if (estado == 2 && Input.GetKeyDown (KeyCode.Space)) {
					falaController.stopDialog ();

					string[] opcoes = new string[4];
					opcoes [0] = "sim";
					opcoes [1] = "nao";
					opcoes [2] = "mais ou menos";
					opcoes [3] = "absolutamente";

					optionSelected = 0;

					falaController.showQuestionDialog ("voce gostou da fala?", opcoes, optionSelected);

					estado = 3;
			} else if (estado == 3 && Input.GetAxis ("Horizontal") != 0) {
					if (Input.GetAxis ("Horizontal") < 0) {
						optionSelected -=1;
					} else if (Input.GetAxis ("Horizontal") > 0) {
						optionSelected +=1;
					}
					if( optionSelected < 0) optionSelected = 3;
					if( optionSelected > 3) optionSelected = 0;
					falaController.setOptionSlected (optionSelected);

			}  else if (estado == 3 && Input.GetKeyDown (KeyCode.Space)) {
					falaController.stopDialog ();
					estado = 1;
                this.talkingTo.GetComponent<PlayerController>().CanMove = true;
			}
		}
	}


	void OnCollisionEnter2D(Collision2D objeto){
		if( objeto.gameObject.GetComponent<PlayerController>() != null ){
			this.talkingTo = objeto.gameObject;
			estado = 1;	
		}
	}

	void OnCollisionExit2D(Collision2D objeto){
		if( objeto.gameObject.GetComponent<PlayerController>() != null ){
			estado = IDLE;
		}
	}
}
