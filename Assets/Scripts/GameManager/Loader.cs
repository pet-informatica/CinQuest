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
	public GameObject npcListener;
	public GameObject gameStateMachine;

	void Awake () 
	{
		if (GameManager.Instance == null)
			Instantiate (gameManager, Vector3.zero, Quaternion.identity);
        if (User.Instance == null)
            Instantiate(user, Vector3.zero, Quaternion.identity);
		if (NPCListener.Instance == null)
			Instantiate(npcListener, Vector3.zero, Quaternion.identity);
		if (GameStateMachine.Instance == null)
			Instantiate(gameStateMachine, Vector3.zero, Quaternion.identity);
    }
}
