using UnityEngine;
using System.Collections;

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
    IEnumerator TeleportRoutine(GameObject chatacter, bool fade, Transform destination)
    {
        transporting = true;

        if (fade)
            yield return new WaitForSeconds(fader.BeginFade(1, 0.3f));

        chatacter.transform.position = destination.position;

        if (fade)
            fader.BeginFade(-1, 0.3f);

        transporting = false;
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
        StartCoroutine(TeleportRoutine(character, true, destination));
        return true;
    }
}
