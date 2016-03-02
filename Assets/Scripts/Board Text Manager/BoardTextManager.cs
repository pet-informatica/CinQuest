using UnityEngine;
using System.Collections;

public class BoardTextManager : MonoBehaviour {

    //public GUIContent content;
    public Texture2D textBox;
    public GUIStyle style;
    private bool activated;

	// Use this for initialization
	void Start () {
        activated = false;
        
	}
	
	//
	void OnGUI () {
        if(activated)
        {
            //AutoResize(1024, 768);

            GUI.Box(new Rect(0.0f, 0.0f, Screen.width, Screen.height), textBox, style);
            //GUI.DrawTexture(new Rect(0.0f, 0.0f, textBox.width, textBox.height), textBox);
            GUI.skin.box.alignment = TextAnchor.LowerCenter;
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="authorWidth"></param>
    /// <param name="authorHeight"></param>
    public static void AutoResize(int authorWidth, int authorHeight)
    {
        Vector2 resizeVector = new Vector2 ((float)Screen.width / authorWidth, (float)Screen.height / authorHeight);

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeVector.x, resizeVector.y, 1.0f));
    }
}
