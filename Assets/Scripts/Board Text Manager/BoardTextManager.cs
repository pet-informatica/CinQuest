using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controls the board text actions
/// </summary>
public class BoardTextManager : MonoBehaviour {

    public Texture2D textBox;
    public TextAsset theText;
    public int line1;
    public int line2;
    private string[] textLines;
    public GUIStyle style1;
    public GUIStyle style2;
    private bool activated;


	void Start () {
        textLines = theText.text.Split('\n');
        activated = false;
	}

	/// <summary>
	/// Board's text appaers when is allowers 
	/// </summary>
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

	/// <summary>
	/// Board's text is activated when the player is on the colision arear of the board
	/// </summary>
	/// <param name="other">Other.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
		if(other.name == "Player")
        {
            activated = true;
        }
    }

	/// <summary>
	/// Board's text is disabled when the player left on the colision arear of the board
	/// </summary>
	/// <param name="other">Other.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            activated = false;
        }
    }
    
}
