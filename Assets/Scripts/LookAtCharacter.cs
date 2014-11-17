using UnityEngine;
using System.Collections;


/*
 * USO: Use Este script para que o NPC olhe para o Character (player) quando iniciar uma conversa
 */

public class LookAtCharacter : MonoBehaviour {

	public Sprite lookingDown; 	//Sprite quando o NPC olha para baixo
	public Sprite lookingUp; 	//Sprite quando o NPC olha para cima
	public Sprite lookingRight; //Sprite quando o NPC olha para direita
	public Sprite lookingLeft;	//Sprite quando o NPC olha para esquerda

	private SpriteRenderer spriteRenderer; 

	// quando inicia conversa 
	void OnCollisionEnter2D(Collision2D objeto){
		if( Input.GetButtonDown ("Fire1") ){
			updateSprite(objeto);
		}
	}

	// quando inicia conversa 
	void OnCollisionStay2D(Collision2D objeto){
		if( Input.GetButtonDown ("Fire1") ){
			updateSprite(objeto);
		}
	}

	
	private void updateSprite(Collision2D objeto){
		float deltaX = objeto.transform.position.x - transform.position.x;
		float deltaY = objeto.transform.position.y - transform.position.y;
		Debug.Log (deltaX);
		Debug.Log (deltaY);

		if ( Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) { //character esta do lado 
			if( deltaX > 0){
				spriteRenderer.sprite = lookingRight;
			} else {
				spriteRenderer.sprite = lookingLeft;
			}
		} else { //character acima ou abaixo
			if( deltaY > 0){
				spriteRenderer.sprite = lookingUp;
			} else {
				spriteRenderer.sprite = lookingDown;
			}
		}
	}

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
