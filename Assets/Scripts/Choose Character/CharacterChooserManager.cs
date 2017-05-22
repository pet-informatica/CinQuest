using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Character chooser manager.
/// </summary>
public class CharacterChooserManager : MonoBehaviour {

	SceneChanger sceneChanger;

	public GameObject[] optionsPanels;
	public GameObject leftArrow;
	public GameObject rightArrow;
	public GameObject selected;
	public GameObject nickname;

	private int currentOptionPanel;
	private static GameObject selectedCharacter;

	void Awake() {
		gameObject.AddComponent<SceneChanger>();
		sceneChanger = GetComponent<SceneChanger>();
		sceneChanger.destinyScene = "CinParking";
	}

	void Start () {
		currentOptionPanel = 0;
		leftArrow.SetActive (false);
		if (optionsPanels.Length > 1) {
			rightArrow.SetActive (true);
		}
	}

	void Update() {
//		print (currentOptionPanel);
	}

	/// <summary>
	/// Selects the character.
	/// </summary>
	/// <param name="character">Character.</param>
	public static void selectCharacter(GameObject character) {
		selectedCharacter = character;
	}

	/// <summary>
	/// Changes the panel to the right using Lerp and shows another set of character
	/// </summary>
	public void nextCharacters() {
		leftArrow.SetActive (true);
		selected.SetActive (false);
		selectedCharacter = null;

		if (currentOptionPanel + 1 < optionsPanels.Length) {
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(-1065, 60, 0)));

			currentOptionPanel++;

			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(0, 60, 0)));

			if (currentOptionPanel == optionsPanels.Length -1) {
				rightArrow.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Changes the panel to the left using Lerp and shows another set of character
	/// </summary>
	public void previousCharacters() {
		rightArrow.SetActive (true);
		selected.SetActive (false);
		selectedCharacter = null;

		if (currentOptionPanel - 1 >= 0) {
			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(1065, 60, 0)));

			currentOptionPanel--;

			StartCoroutine(MoveFunction(optionsPanels [currentOptionPanel], new Vector3(0, 60, 0)));

			if (currentOptionPanel == 0) {
				leftArrow.SetActive (false);
			}
		}
	}

	/// <summary>
	/// Moves object smoothly from one position to another
	/// </summary>
	/// <param name="Object">Object.</param>
	/// <param name="newPosition">New position.</param>
	IEnumerator MoveFunction(GameObject obj, Vector3 newPosition)
	{
		float timeSinceStarted = 0f;
		while (true)
		{
			timeSinceStarted += Time.deltaTime * 2.0f;
			obj.GetComponent<RectTransform> ().localPosition = 
				Vector3.Lerp(obj.GetComponent<RectTransform> ().localPosition, newPosition, timeSinceStarted);

			if (obj.GetComponent<RectTransform> ().localPosition == newPosition)
			{
				yield break;
			}
				
			yield return null;
		}
	}

	/// <summary>
	/// Starts the game with the nickname provide when choose the character.
	/// </summary>
	public void StartGame() {
		GameManager.Instance.gameData.PlayerName = nickname.GetComponent<Text> ().text;
		GameManager.Instance.SaveGame ();
		sceneChanger.Change();
	}
}
