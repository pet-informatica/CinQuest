using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Contains a dictionary of MapObjects for serving as a data base for the global map
/// Developed by: Higor (hcmb)
/// </summary>
public class MapRepository
{

    Dictionary<string, MapObject> objects = new Dictionary<string, MapObject>();

    public MapRepository()
    {
        objects.Add("Cin", new MapObject("Centro de Informática"));
        objects.Add("NewCin", new MapObject("Centro de Informática - Prédio Novo"));
    }

    /// <summary>
    /// Gets an map object from the repository by id
    /// </summary>
    /// <param name="id">The string id of the object</param>
    /// <returns>The MapObject associated with the id</returns>
    public MapObject Get(string id)
    {
        if (objects.ContainsKey(id))
            return objects[id];
        return null;
    }
}
