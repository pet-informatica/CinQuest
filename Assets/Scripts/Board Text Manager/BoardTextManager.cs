using UnityEngine;
using System.Collections;

public class BoardTextManager : MonoBehaviour {

    public GUIContent content;
    public GUIStyle style;
    private bool activated;

	// Use this for initialization
	void Start () {
        activated = false;
        
	}
	
	// Update is called once per frame
	void OnGUI () {
        if(activated)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), content, style);
        }
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            activated = true;
            print("hellooo");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            activated = false;
        }
    }
}
