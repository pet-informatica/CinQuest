using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Character slot.
/// </summary>
public class CharacterSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public GameObject selected;
	public GameObject current;

	/// <summary>
	/// Raises the pointer enter event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerEnter (PointerEventData eventData)
	{
		current.SetActive (true);
		current.transform.position = gameObject.transform.position;
	}
		
	/// <summary>
	/// Close the cahracter slot.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerExit (PointerEventData eventData)
	{
		current.SetActive (false);
	}
		
	/// <summary>
	/// Raises the pointer click event.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick (PointerEventData eventData)
	{
		selected.SetActive (true);
		selected.transform.position = gameObject.transform.position;
		CharacterChooserManager.selectCharacter (gameObject);
	}
}
