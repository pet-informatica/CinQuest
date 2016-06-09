using UnityEngine;
using System.Collections;

public class Item : GenericItem {

    public string title;
    public string description;

    public Item(int identifier, string name, string description) : base(identifier)
    {
        this.title = name;
        this.description = description;
    }
}
