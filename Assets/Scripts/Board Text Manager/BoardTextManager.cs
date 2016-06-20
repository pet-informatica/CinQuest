using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardTextManager : MonoBehaviour {
    /*
        Developed by: Lucas (lss5)
        Description:
        How to use it:
        */
    public Texture2D textBox;
    public TextAsset theText;
    public int line1;
    public int line2;
    private string[] textLines;
    public GUIStyle style1;
    public GUIStyle style2;
    private bool activated;

	// Use this for initialization
	void Start () {
        textLines = theText.text.Split('\n');
        activated = false;
	}
	
	//
	void OnGUI () {
        if(activated)
        {
            //theText = "A001 - Silvio Melo\nsmb@cin.ufpe.br";//Criar o txt e fazer ele pegar das strings. eo standart
            GameManager.AutoResize();
            GUI.Box(new Rect(0.0f, 0.0f, textBox.width, textBox.height), textBox, style1);
            GUI.Box(new Rect(0.0f, 0.0f, textBox.width, textBox.height), textLines[line1]+"\n"+textLines[line2], style2);
            style2.normal.textColor = Color.white; 
        }
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.name == "Player")
        {
            activated = true;
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
