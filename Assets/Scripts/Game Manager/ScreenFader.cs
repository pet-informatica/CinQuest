using UnityEngine;
using System.Collections;

/// <summary>
/// Developed by: Higor (hcmb)
/// ScreenFader can be used for fading the screen in/out whenever needed.
/// This script must be attached to the GameManager prefab to work.
/// </summary>
public class ScreenFader : MonoBehaviour 
{
	public Texture2D texture;

	float fadeTime = 1.2f;
	int drawDepth = -100;
	float alpha = 1.0f;
	int fadeDirection = -1;

	void OnGUI()
	{
		alpha += fadeDirection * (1f/fadeTime) * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0f, 0f, Screen.width, Screen.height), texture);
	}

    /// <summary>
    /// Start fading the screen in or out
    /// </summary>
    /// <returns>The time it takes for fading in seconds</returns>
    /// <param name="direction">Use 1 for fading in, and -1 for fading out</param>
    /// <param name="speed">The time in seconds for the fade to finish.</param>
    public float BeginFade(int direction, float time)
    {
        fadeDirection = direction;
        fadeTime = time;
        return fadeTime;
    }

    /// <summary>
    /// Fade out everytime a new scene is loaded.
    /// </summary>
    void OnLevelWasLoaded()
	{
		BeginFade (-1, 1.2f);
	}
}
