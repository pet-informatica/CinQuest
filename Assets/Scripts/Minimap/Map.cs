using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
        ClampPosition();
    }

    /// <summary>
    /// Dont let the content get out of bounds inside canvas.
    /// </summary>
    void ClampPosition()
    {
        float twidth = (contentRect.rect.width) / 2.0f;
        float theight = (contentRect.rect.height) / 2.0f;
        twidth *= minZoom;
        theight *= minZoom;
        float cwidth = (contentRect.rect.width) * contentRect.localScale.x;
        float cheight = (contentRect.rect.height) * contentRect.localScale.y;
        float cx = contentRect.anchoredPosition.x;
        float cy = contentRect.anchoredPosition.y;
        float margin = Mathf.Pow(contentRect.localScale.x, 14);
        Vector3 npos = new Vector3(Mathf.Clamp(cx, -Mathf.Abs(twidth - cwidth)- margin, Mathf.Abs(twidth - cwidth)+margin),
                                   Mathf.Clamp(cy, -Mathf.Abs(theight - cheight)- margin, Mathf.Abs(theight - cheight)+ margin), 0);
        contentRect.anchoredPosition = npos;
                    
                                    
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
            if (Minimap.Instance != null)
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
            if(Minimap.Instance != null)
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

    /// <summary>
    /// Called when the mouse is clicked. Useful for dragging UI.
    /// </summary>
    public void OnDrag(BaseEventData eventData)
    {
        var pointerData = eventData as PointerEventData;

        if (pointerData == null) { return; }


        var currentPosition = contentRect.position;
        currentPosition.x += pointerData.delta.x;
        currentPosition.y += pointerData.delta.y;
        contentRect.position = currentPosition;
    }
}