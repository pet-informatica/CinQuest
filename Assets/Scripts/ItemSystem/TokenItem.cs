using UnityEngine;
using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Definition for a not showable item.
/// Could contain differents properties.
/// </summary>
[CreateAssetMenu(menuName = "ItemsSystem/Token")]
[Serializable]
public class TokenItem : GenericItem
{
	public bool active;
	
}