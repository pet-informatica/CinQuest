using UnityEngine;
using System.Collections;

/// <summary>
/// Represents an item. Composed by a sprite, a title and description
/// </summary>
[CreateAssetMenu]
public class Item : GenericItem {

	public Sprite sprite;
    public string title;
    public string description;

	/// <summary>
	/// Initializes a new instance of the <see cref="Item"/> class.
	/// </summary>
	/// <param name="identifier">Identifier.</param>
	/// <param name="sprite">Sprite.</param>
	/// <param name="title">Title.</param>
	/// <param name="description">Description.</param>
    public Item(int identifier, Sprite sprite, string title, string description) : base(identifier)
    {
		this.sprite = sprite;
		this.title = title;
        this.description = description;
    }
}
