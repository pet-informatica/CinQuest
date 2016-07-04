using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// The Global Map instanced classe
/// Developed by: Higor (hcmb)
/// </summary>
public class Map : MonoBehaviour
{
    public AudioClip openingSound;
    public GameObject UI;
    public GameObject content;
    public GameObject description;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;
    public float zoomSpeed = 5.0f;
    RectTransform contentRect;
    RectTransform descriptionRect;
    RectTransform descriptionTextRect;
    Text descriptionText;
    bool objectHovered;

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

    private MapRepository model;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            model = new MapRepository();
            instance = this;
            IsOpen = true;
            contentRect = content.GetComponent<RectTransform>();
            descriptionRect = description.GetComponent<RectTransform>();
            descriptionText = description.GetComponentInChildren<Text>();
            descriptionTextRect = descriptionText.GetComponent<RectTransform>();
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

        if (objectHovered)
            UpdateDescription();
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
    /// <param name="eventData">The eventData associated with unity UI EventSystem</param>
    public void OnDrag(BaseEventData eventData)
    {
        var pointerData = eventData as PointerEventData;

        if (pointerData == null) { return; }

        var currentPosition = contentRect.position;
        currentPosition.x += pointerData.delta.x;
        currentPosition.y += pointerData.delta.y;
        contentRect.position = currentPosition;
    }

    /// <summary>
    /// Called by EventSystem when a MapObject is hovered. Receives the eventData and communicates with
    /// the model repository for getting the MapObject info and then opens it in the description UI
    /// </summary>
    /// <param name="eventData">The eventData associated with unity UI EventSystem</param>
    public void ShowDescription(BaseEventData eventData)
    {
        var pointerData = eventData as PointerEventData;
        string hovered = pointerData.pointerEnter.name;
        description.SetActive(true);
        descriptionText.text = model.Get(hovered).Title;
        objectHovered = true;
    }

    /// <summary>
    /// Updates the description UI for matching mouse position when it's active.
    /// </summary>
    public void UpdateDescription()
    {
        Vector2 p = descriptionRect.position;
        p = Input.mousePosition;
        p.y += 25f;
        descriptionRect.position = p;
        descriptionRect.sizeDelta = new Vector2(descriptionRect.sizeDelta.x, descriptionTextRect.rect.height + 25f);
    }

    /// <summary>
    /// Called by EventSystem when a MapObject is hovered. Hides the description UI.
    /// </summary>
    /// <param name="eventData">The eventData associated with unity UI EventSystem</param>
    public void HideDescription(BaseEventData eventData)
    {
        description.SetActive(false);
        objectHovered = false;
    }
}