using UnityEngine;
using System.Collections;

/// <summary>
/// Represents an interative object that can be placed in the global map and minimap
/// Developed by: Higor (hcmb)
/// </summary>
public class MapObject
{

    string title;
    /// <summary>
    /// The name of the object
    /// </summary>
    public string Title
    {
        get { return title; }
    }

    /// <summary>
    /// Constructs a new MapObject
    /// </summary>
    /// <param name="title">The title for the new MapObject</param>
    public MapObject(string title)
    {
        this.title = title;
    }
}
