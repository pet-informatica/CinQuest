using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	public bool leftArrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextCharacters() {
		if (leftArrow) {
			print ("pra esquerda");
		} else {
			print ("pra direita");
		}
	}
}
