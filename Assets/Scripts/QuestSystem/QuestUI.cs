using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class QuestUI : MonoBehaviour {

    public GameObject canvas;
    bool opened;
	
	void Start ()
    {
        CloseWindow();
	}

    public void OpenWindow()
    {
        Cursor.visible = true;
        canvas.SetActive(true);
        opened = true;
    }

    public void CloseWindow()
    {
        //Cursor.visible = false;
        canvas.SetActive(false);
        opened = false;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (opened) CloseWindow();
            else OpenWindow();
        }
	}
}
