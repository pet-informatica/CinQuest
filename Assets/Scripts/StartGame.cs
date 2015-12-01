using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	GameObject cinquest;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey) {
			Application.LoadLevel(1);
		}
	}
	
	void Awake() {
		cinquest = GameObject.Find ("cinquest");
		
		StartCoroutine (BlinkImage());
	}
	
	IEnumerator BlinkImage() {
		while (true) {
			print ("vai");
			Color color = cinquest.GetComponent<Renderer> ().material.color;
			
			color.a = 0f;
			cinquest.GetComponent<Renderer> ().material.color = color;
			yield return new WaitForSeconds (0.5f);
			
			color.a = 1f;
			cinquest.GetComponent<Renderer> ().material.color = color;
			print ("foi");
			yield return new WaitForSeconds (0.5f);
		}
	}
}
