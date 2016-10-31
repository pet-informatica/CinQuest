using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// Telepor
/// </summary>
public class Teleporter : MonoBehaviour
{
    ScreenFader fader;
    bool transporting;

    public void Start()
    {
        fader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScreenFader>();
    }

    /// <summary>
    /// Call fade in and sleep until it's done.
    /// </summary>
    IEnumerator TeleportRoutine(List<GameObject> characters, bool fade, List<Transform> destination)
    {
        transporting = true;

        if (fade)
            yield return new WaitForSeconds(fader.BeginFade(1, 0.3f));

		for (int i = 0; i < characters.Count; ++i)
			characters[i].transform.position = destination [i].position;

        if (fade)
            fader.BeginFade(-1, 0.3f);

        transporting = false;
    }

    /// <summary>
    /// Teleports a character group for a target destination
    /// </summary>
    /// <param name="character">The character gameObject you want to transport.</param>
    /// <param name="fade">Set true if you want to fade the screen in the process.</param>
    /// <param name="destination">The transform with the destination position</param>
    /// <returns>True if the teleport was sucessful. Flase otherwise if it couldn't happen.</returns>
	public bool Teleport(List<GameObject> characters, bool fade, List<Transform> destination)
    {
        if (transporting)
            return false;
		StartCoroutine(TeleportRoutine(characters, true, destination));
        return true;
    }

	/// <summary>
	/// Teleports a character for a target destination
	/// </summary>
	/// <param name="character">The character gameObject you want to transport.</param>
	/// <param name="fade">Set true if you want to fade the screen in the process.</param>
	/// <param name="destination">The transform with the destination position</param>
	/// <returns>True if the teleport was sucessful. Flase otherwise if it couldn't happen.</returns>
	public bool Teleport(GameObject character, bool fade, Transform destination)
	{
		if (transporting)
			return false;
		List<GameObject> tmp1 = new List<GameObject> ();
		tmp1.Add (character);
		List<Transform> tmp2 = new List<Transform> ();
		tmp2.Add (destination);
		StartCoroutine(TeleportRoutine(tmp1, true, tmp2));
		return true;
	}
}
