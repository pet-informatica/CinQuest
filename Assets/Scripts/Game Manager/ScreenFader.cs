using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour 
{
	/*

		Developed by: Higor

		Description: The ScreenFader is responsible for making a FadeIn/FadeOut effect whenever we need it.
		Generally, it will be used to fade when changing scenes.

		How to use it: This script MUST be attached to the GameManager prefab. Put a black texture on it, and
		set the fadeSpeed (The fade speed is the exact time in seconds, so use 1 for 1 second off fade in for example).
	 */

	public Texture2D texture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -100;
	private float alpha = 1.0f;
	private int fadeDirection = -1;

	void OnGUI()
	{
		alpha += fadeDirection * fadeSpeed * Time.deltaTime;
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
	public float BeginFade(int direction)
	{
		fadeDirection = direction;
		return fadeSpeed;
	}

	//Fade out everytime a new scene is loaded.
	void OnLevelWasLoaded()
	{
		BeginFade (-1);
	}
}
