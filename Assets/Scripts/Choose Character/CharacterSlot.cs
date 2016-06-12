using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public GameObject selected;
	public GameObject current;

	public void OnPointerEnter (PointerEventData eventData)
	{
		current.SetActive (true);
		current.transform.position = gameObject.transform.position;
	}
		
	public void OnPointerExit (PointerEventData eventData)
	{
		current.SetActive (false);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		selected.SetActive (true);
		selected.transform.position = gameObject.transform.position;
		CharacterChooserManager.selectCharacter (gameObject);
	}
}
