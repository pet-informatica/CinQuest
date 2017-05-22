using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Play sounds when triggering click/hover events on an object
/// </summary>
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {

	public string hoveredFX = "ButtonHovered";
	public string clickedFX = "ButtonClicked";

	/// <summary>
	/// Call the MusicPlayer.PlayFX method to play a sound from your ID when a click/hover event is triggered.
	/// </summary>
	/// <param name="data">Data.</param>
	public void OnPointerEnter(PointerEventData data){
		MusicPlayer.Instance.PlayFX (hoveredFX);
	}
		
	/// <summary>
	/// Call the MusicPlayer.PlayFX method to play a sound from your ID when a click/hover event is triggered.
	/// </summary>
	/// <param name="data">Data.</param>
	public void OnPointerClick(PointerEventData data){
		MusicPlayer.Instance.PlayFX (clickedFX);
	}
}
