using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {

	public string hoveredFX = "ButtonHovered";
	public string clickedFX = "ButtonClicked";

	public void OnPointerEnter(PointerEventData data){
		MusicPlayer.Instance.PlayFX (hoveredFX);
	}

	public void OnPointerClick(PointerEventData data){
		MusicPlayer.Instance.PlayFX (clickedFX);
	}
}
