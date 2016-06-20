using UnityEngine;

/// <summary>
/// Developed by: Higor (hcmb)
/// Instantiates the singletons in scene. Must be attached to main camera, and every camera
/// in every scene must have it.
/// </summary>
public class Loader : MonoBehaviour 
{
	public GameObject gameManager;
    public GameObject user;

	void Awake () 
	{
		if (GameManager.Instance == null)
			Instantiate (gameManager, Vector3.zero, Quaternion.identity);
        if (User.Instance == null)
            Instantiate(user, Vector3.zero, Quaternion.identity);
    }
}
