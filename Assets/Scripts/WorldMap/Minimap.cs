using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// The minimap class
/// Developed by: Higor (hcmb)
/// </summary>
public class Minimap : MonoBehaviour {

    public GameObject UI;
    Text title;

    private static Minimap instance = null;

    /// <summary>
    /// The minimap static instace for global remote access
    /// </summary>
    public static Minimap Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Returns if the minimap is currently on scene
    /// </summary>
    public bool IsOpen
    {
        get; private set;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            IsOpen = true;
        }
    }

	void Start () {
        title = GetComponentInChildren<Text>();
        title.text = SceneNamer.UI(SceneManager.GetActiveScene().name);
    }

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		UpdateTitle();
	}

    void UpdateTitle()
    {
        if (SceneNamer.UI(SceneManager.GetActiveScene().name) != null)
            if (title != null)
                title.text = SceneNamer.UI(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Opens the global map.
    /// </summary>
    public void Open()
    {
        if (checkIfMapIsOnScene() && !IsOpen)
        {
            IsOpen = true;
            UI.SetActive(true);
            UpdateTitle();
        }
    }

    /// <summary>
    /// Closes the info.
    /// </summary>
    public void Close()
    {
        if (checkIfMapIsOnScene() && IsOpen)
        {
            IsOpen = false;
            UI.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if alert box is on scene.
    /// </summary>
    /// <returns><c>true</c>, if if alert is on scene, <c>false</c> otherwise.</returns>
    private bool checkIfMapIsOnScene()
    {
        if (UI == null)
            throw new System.NullReferenceException();
        return true;
    }
}
