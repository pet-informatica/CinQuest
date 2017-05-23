using UnityEngine;
using System.Collections;

/// <summary>
/// Changes an object Sprite when player interacts with it, for facing up, down, left or right depending on where the player is
/// </summary>
public class LookAtCharacter : MonoBehaviour {

	public Sprite lookingDown; 
	public Sprite lookingUp; 	
	public Sprite lookingRight; 
	public Sprite lookingLeft;	
    Sprite origin;
    public bool backToOrigin = true;

	private SpriteRenderer spriteRenderer; 

	/// <summary>
	/// Updates the object sprite to face player when interaction button is pressed and player is nearby
	/// </summary>
	/// <param name="objeto">The collider that is nearby. For checking if it's really the player.</param>
	void OnTriggerStay2D(Collider2D col){
		if(Input.GetButtonDown("Interaction") && col.gameObject.tag == "PlayerFront"){
			updateSprite(col);
		}
	}

	/// <summary>
	/// Get's the sprite back to original position
	/// </summary>
	/// <param name="objeto">The collider that is nearby. For checking if it's really the player</param>
    void OnTriggerExit2D(Collider2D col)
    {
		if(backToOrigin && col.gameObject.tag == "PlayerFront")
       	 spriteRenderer.sprite = origin;
    }

	/// <summary>
	/// Check if the player is above, bellow, to the left or to the right of the object and updates it's sprite accordingly
	/// </summary>
	/// <param name="objeto">The payer collider gameobject</param>
	private void updateSprite(Collider2D objeto){
		float deltaX = objeto.transform.position.x - transform.position.x;
		float deltaY = objeto.transform.position.y - transform.position.y;

		if ( Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) { //Character is to the left or to the right
			if( deltaX > 0){
				spriteRenderer.sprite = lookingRight;
			} else {
				spriteRenderer.sprite = lookingLeft;
			}
		} else { //Character is above or bellow
			if( deltaY > 0){
				spriteRenderer.sprite = lookingUp;
			} else {
				spriteRenderer.sprite = lookingDown;
			}
		}
	}

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
        origin = spriteRenderer.sprite;
	}
}
