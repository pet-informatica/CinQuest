using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controls the camera, tracking the player.
/// Desenvolvido por: Higor
/// </summary>
public class CameraController : MonoBehaviour
{
    public bool followingPlayerX;
    public bool followingPlayerY;
    public float followSpeed = 7.5f;
    Transform player;

	void Start ()
    {
        followingPlayerX = true;
        followingPlayerY = true;
        if (GameObject.FindGameObjectWithTag("Player") != null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

	/// <summary>
	/// Sets the player transform to track, and tracking flags to true
	/// </summary>
	void TrackPlayer(){
		followingPlayerX = true;
		followingPlayerY = true;
		if(GameObject.FindGameObjectWithTag("Player") != null)
			player = GameObject.FindGameObjectWithTag("Player").transform;
	}
		
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
		TrackPlayer ();
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		TrackPlayer ();
	}

	void Update ()
    {
        if(player != null)
        {
            Vector3 target = transform.position;
            if (followingPlayerX) target.x = player.transform.position.x;
            if (followingPlayerY) target.y = player.transform.position.y;
            transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
        }
	}
}
