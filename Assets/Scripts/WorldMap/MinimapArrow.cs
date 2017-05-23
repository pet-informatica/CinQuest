using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Tracks the player animator to rotate an arrow in the minimap according to the updated player moving direction
/// </summary>
public class MinimapArrow : MonoBehaviour
{

    Animator anim;

    void Awake()
    {
		TrackPlayer ();
    }

	/// <summary>
	/// Get the player animator
	/// </summary>
	void TrackPlayer(){
		if(GameObject.FindGameObjectWithTag("Player") != null)
			anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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

	/// <summary>
	/// Checks the animator horizontal/vertical speed to discover the player facing direction and rotates the arrow accordingly
	/// </summary>
    void UpdateSensorPosition()
    {
        if (anim == null)
            return;

        if (anim.GetFloat("VerticalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 180);
        else if (anim.GetFloat("VerticalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (anim.GetFloat("HorizontalSpeed") > 0.1f)
            transform.eulerAngles = new Vector3(0, 0, 90);
        else if (anim.GetFloat("HorizontalSpeed") < -0.1f)
            transform.eulerAngles = new Vector3(0, 0, 270);
    }

    void Update()
    {
        UpdateSensorPosition();
    }
}
