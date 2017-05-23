using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Manages all the sound fx in the game.
/// </summary>
public class MusicPlayer : MonoBehaviour {

	[System.Serializable]
	public struct Song{
		public string scene;
		public AudioClip audio;
	}

	public Song[] songs;

	[System.Serializable]
	public struct Effect{
		public string name;
		public AudioClip audio;
	}

	public Effect[] effects;

	AudioSource player;
	Dictionary<string, AudioClip> sceneSong;
	Dictionary<string, AudioClip> soundFX;

	float volumeTime;
	int volumeDirection;
	string currentSong;

	static MusicPlayer instance = null;
	/// <summary>
	/// The true instance for static accesing this class resources.
	/// </summary>

	public static MusicPlayer Instance
	{
		get { return instance; }
	}

	void Awake () {
		if (instance == null) {
			LoadPlayer ();
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// Gets the list of sounds of songs from the inspector and creates a hash table from it
	/// </summary>
	void LoadPlayer(){
		instance = this;
		volumeDirection = 1;
		volumeTime = 1.2f;
		player = GetComponent<AudioSource> ();
		sceneSong = new Dictionary<string, AudioClip> ();
		soundFX = new Dictionary<string, AudioClip> ();
		foreach (Song song in songs)
			sceneSong.Add (song.scene, song.audio);
		foreach (Effect fx in effects)
			soundFX.Add (fx.name, fx.audio);
	}

	/// <summary>
	/// Play an effect one shot
	/// </summary>
	/// <param name="name">the fx id</param>
	public void PlayFX(string name){
		if (soundFX.ContainsKey (name))
			player.PlayOneShot (soundFX [name]);
	}

	/// <summary>
	/// Changes the song with a fade effect
	/// </summary>
	/// <param name="scene">song id</param>
	/// <param name="time">fade time</param>
	public void ChangeSong(string scene, float time){
		if(sceneSong.ContainsKey(scene) && currentSong != scene)
			StartCoroutine (Change (scene, time));
	}

	/// <summary>
	/// Change the background song with a fade effect
	/// </summary>
	/// <param name="scene">new song id</param>
	/// <param name="time">fade time</param>
	IEnumerator Change(string scene, float time){
		volumeDirection = -1;
		volumeTime = time;
		yield return new WaitForSeconds (time);
		currentSong = scene;
		player.clip = sceneSong [scene];
		player.Play ();
		volumeDirection = 1;
	}

	void Update () {
		player.volume += volumeDirection * (1f/volumeTime) * Time.deltaTime;
		player.volume = Mathf.Clamp01 (player.volume);
	}
}
