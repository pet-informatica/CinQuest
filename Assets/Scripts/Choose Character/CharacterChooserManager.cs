using UnityEngine;
using System.Collections;

public class CharacterChooserManager : MonoBehaviour {

	public GameObject[] optionsPanels;

	private int currentOptionPanel = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextCharacters() {
		if (currentOptionPanel + 1 < optionsPanels.Length) {
//			optionsPanels [currentOptionPanel].GetComponent<RectTransform> ().localPosition = new Vector3 (-1065, 0, 0);

			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(-1065, 0, 0)));
			currentOptionPanel++;

//			optionsPanels [currentOptionPanel].GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(0, 0, 0)));
		}
	}

	public void previousCharacters() {
		if (currentOptionPanel - 1 >= 0) {
//			optionsPanels [currentOptionPanel].GetComponent<RectTransform> ().localPosition = new Vector3 (1065, 0, 0);
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(1065, 0, 0)));
			currentOptionPanel--;
//			optionsPanels [currentOptionPanel].GetComponent<RectTransform> ().localPosition = new Vector3 (0, 0, 0);
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(0, 0, 0)));
		}
	}

	IEnumerator MoveFunction(GameObject panel, Vector3 newPosition)
	{
		float timeSinceStarted = 0f;
		while (true)
		{
			timeSinceStarted += Time.deltaTime * 2.0f;
			panel.GetComponent<RectTransform> ().localPosition = 
				Vector3.Lerp(panel.GetComponent<RectTransform> ().localPosition, newPosition, timeSinceStarted);

			if (panel.GetComponent<RectTransform> ().localPosition == newPosition)
			{
				yield break;
			}
				
			yield return null;
		}
	}
}
