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
	/// Shows the current slot where the mouse is pointed
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerEnter (PointerEventData eventData)
	{
		current.SetActive (true);
		current.transform.position = gameObject.transform.position;
	}
		
	/// <summary>
	/// Misses the slot where the mouse was pointed to earlier
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerExit (PointerEventData eventData)
	{
		current.SetActive (false);
	}
		
	/// <summary>
	/// Shows the select slot where the mouse clicked and storage the character
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick (PointerEventData eventData)
	{
		selected.SetActive (true);
		selected.transform.position = gameObject.transform.position;
		CharacterChooserManager.selectCharacter (gameObject);
	}
}
