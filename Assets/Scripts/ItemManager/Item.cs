using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class Item : GenericItem {

	public Sprite sprite;
    public string title;
    public string description;

    public Item(int identifier, Sprite sprite, string title, string description) : base(identifier)
    {
		this.sprite = sprite;
		this.title = title;
        this.description = description;
    }
}
