using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPointerEnter (PointerEventData eventData)
	{
		GameObject.Find ("Selected").transform.position = gameObject.transform.position;
	}
		
	public void OnPointerExit (PointerEventData eventData)
	{
		
	}
}
