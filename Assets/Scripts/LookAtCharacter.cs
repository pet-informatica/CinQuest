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
    Sprite origin;
    public bool backToOrigin = true;

	private SpriteRenderer spriteRenderer; 

	// quando inicia conversa 
	void OnTriggerStay2D(Collider2D objeto){
		if( Input.GetKey (KeyCode.Z) ){
			updateSprite(objeto);
		}
	}

    void OnTriggerExit2D(Collider2D objeto)
    {
        spriteRenderer.sprite = origin;
    }

	private void updateSprite(Collider2D objeto){
		float deltaX = objeto.transform.position.x - transform.position.x;
		float deltaY = objeto.transform.position.y - transform.position.y;

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
        origin = spriteRenderer.sprite;
	}
}
