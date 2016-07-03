using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// The Global Map instanced classe
/// Developed by: Higor (hcmb)
/// </summary>
public class Map : MonoBehaviour
{
    public AudioClip openingSound;
    public GameObject UI;
    public GameObject content;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;
    public float zoomSpeed = 5.0f;
    RectTransform contentRect;

    /// <summary>
    /// Returns true if the global map is alredy opened.
    /// </summary>
    public bool IsOpen
    {
        get; private set;
    }

    private static Map instance = null;

    /// <summary>
    /// Gets the unique instance of the Map for static access
    /// </summary>
    public static Map Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            IsOpen = true;
            contentRect = content.GetComponent<RectTransform>();
            Close();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Map"))
        {
            if (IsOpen) Close();
            else Open();
        }

        Scroll();
    }

    /// <summary>
    /// Check for player mouse scroll wheel input for zooming in/out the content of the map.
    /// </summary>
    void Scroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime * 0.001f;

        float x = contentRect.localScale.x;
        float y = contentRect.localScale.y;
        float z = contentRect.localScale.z;
        contentRect.localScale = new Vector3(Mathf.Clamp(x + scroll, minZoom, maxZoom), Mathf.Clamp(y + scroll, minZoom, maxZoom), z);
    }

    /// <summary>
    /// Opens the global map UI panel.
    /// </summary>
    public void Open()
    {
        if (checkIfMapIsOnScene() && !IsOpen)
        {
            IsOpen = true;
            UI.SetActive(true);
            Minimap.Instance.Close();
        }
    }

    /// <summary>
    /// Closes the map UI panel.
    /// </summary>
    public void Close()
    {
        if (checkIfMapIsOnScene() && IsOpen)
        {
            IsOpen = false;
            UI.SetActive(false);
            Minimap.Instance.Open();
        }
    }

    /// <summary>
    /// Checks if alert box is on scene.
    /// </summary>
    /// <returns><c>true</c>, if if alert is on scene, <c>false</c> otherwise.</returns>
    private bool checkIfMapIsOnScene()
    {
        if (UI == null)
            throw new NullReferenceException();
        return true;
    }
}