using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

	public GameObject slotPrefab, itemPrefab;
	public Vector2 inventorySize;
	public float slotSize;
	public Vector2 windowSize;
	public Sprite[] spriteSheet;
	public Text selectedItemTitle;
	public Text selectedItemDescription;
	public Text playerName;
	public Text playerLogin;
	public Text playerCourse;

	// Use this for initialization
	void Start () {

		inventorySize = new Vector2(6, 5);
		for (int i = 1; i <= inventorySize.x; i++) {
			for (int j = 1; j <= inventorySize.y; j++) {
				GameObject slot = Instantiate (slotPrefab) as GameObject;

				ChangeSlotSprite (slot, i, j);

				slot.transform.parent = this.transform;
				slot.transform.localScale = new Vector3 (1, 1, 1);
				slot.name = "Slot_" + i + "_" + j;
				slot.GetComponent<RectTransform> ().anchoredPosition = new Vector3 ((windowSize.x) / (inventorySize.x + 1) * i, (windowSize.y) / (inventorySize.y + 1) * -j, 0);
			}
		}

		Debug.Log ("Parei aqui.");

		User user = User.Instance;
		playerName.text = user.Name;
		playerLogin.text = GenerateLogin (user.Name);
		playerCourse.text = user.Course;
	
		Debug.Log ("Depois aqui");
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeSlotSprite (GameObject slot, int i, int j) {
		
		if (i == 1 && j == 1)
			slot.GetComponent<Image>().sprite = spriteSheet [0];
		else if (i == inventorySize.x && j == 1) 
			slot.GetComponent<Image>().sprite = spriteSheet [2];
		else if (i == 1 && j == inventorySize.y)
			slot.GetComponent<Image>().sprite = spriteSheet [6];
		else if (i == inventorySize.x && j == inventorySize.y) 
			slot.GetComponent<Image>().sprite = spriteSheet [8];
		else if (j == 1)
			slot.GetComponent<Image>().sprite = spriteSheet [1];
		else if (i == 1) 
			slot.GetComponent<Image>().sprite = spriteSheet [3];
		else if (j == inventorySize.y)
			slot.GetComponent<Image>().sprite = spriteSheet [7];
		else if (i == inventorySize.x) 
			slot.GetComponent<Image>().sprite = spriteSheet [5];
		else slot.GetComponent<Image>().sprite = spriteSheet [4];

	}

	string GenerateLogin (string name) {
		string login = "";
		string[] names = name.Split (' ');
		foreach (string str in names) {
			if (str.Length > 3) {
				string temp = str.ToLower ();
				login += temp [0];
			}
		} 

		return login;
	}
}
