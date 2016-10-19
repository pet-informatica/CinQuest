using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    public GameObject description;
	public MapModel model;
	public Text title;
    public float minZoom = 0.5f;
    public float maxZoom = 2.0f;
    public float zoomSpeed = 5.0f;
    RectTransform contentRect;
    RectTransform descriptionRect;
    RectTransform descriptionTextRect;
	GameObject activeStage;
    Text descriptionText;

    bool objectHovered;

    /// <summary>
    /// Returns true if the global map is alredy opened.
    /// </summary>
    public bool IsOpen
    {
        get; private set;
    }

    static Map instance = null;
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
			model.Populate ();
			ChangeStage (SceneManager.GetActiveScene().name);
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
			ChangeStage (SceneManager.GetActiveScene().name);
        }
    }

	/// <summary>
	/// Changes the current world map stage to another based on hash
	/// </summary>
	/// <param name="hash">The target stage formal hash name for searching in a dictionary inside model.</param>
	void ChangeStage(string hash)
	{
		HideDescription ();
		if (activeStage != null)
			activeStage.SetActive (false);
		activeStage = model.FindStage (hash).stage;
		activeStage.SetActive (true);
		contentRect = activeStage.GetComponent<RectTransform>();
		title.text = model.FindStage (hash).title;
	}

	/// <summary>
	/// Search for the name of the parent of a stage in the model dictionary.
	/// </summary>
	/// <returns>The name of the parent of the desired child.</returns>
	/// <param name="hash">The target child formal hash name for searching in dictionary.</param>
	string ParentStage(string hash)
	{
		return model.FindStage(hash).parent;
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
    /// <returns><c>true</c> if alert is on scene, <c>false</c> otherwise.</returns>
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
		descriptionText.text = model.FindObject(hovered).title;
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

	/// <summary>
	/// Hides the object description from UI.
	/// </summary>
	public void HideDescription()
	{
		description.SetActive(false);
		objectHovered = false;
	}

	/// <summary>
	/// Executed when a button inside the map is clicked. Changes the map stage content according to the buttons name.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void ChangeContent(BaseEventData eventData){
		var pointerData = eventData as PointerEventData;
		string name = pointerData.pointerEnter.name;
		ChangeStage (name);
	}

	/// <summary>
	/// Returns to the parent of the current stage
	/// </summary>
	public void GoBack(){
		ChangeStage (ParentStage (activeStage.name));
	}
}