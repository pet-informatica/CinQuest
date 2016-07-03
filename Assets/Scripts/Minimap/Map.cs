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
    public GameObject mapObject;

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
    }

    /// <summary>
    /// Opens the global map UI panel.
    /// </summary>
    public void Open()
    {
        if (checkIfMapIsOnScene() && !IsOpen)
        {
            IsOpen = true;
            mapObject.SetActive(true);
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
            mapObject.SetActive(false);
            Minimap.Instance.Open();
        }
    }

    /// <summary>
    /// Checks if alert box is on scene.
    /// </summary>
    /// <returns><c>true</c>, if if alert is on scene, <c>false</c> otherwise.</returns>
    private bool checkIfMapIsOnScene()
    {
        if (mapObject == null)
            throw new NullReferenceException();
        return true;
    }
}