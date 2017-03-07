using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneChanger : MonoBehaviour 
{

	/*
		Developed by: Higor

		Description: This script is responsible for changing from one scene to another, making a FadeIn/FadeOut effect
		in the process.

		How to use it: Create a prefab with a single BoxCollider2D and attach the script to it. Fill the "destinyScene" variable
        name with the scene you want to go, and name the prefab with exactly the same name. This is important. Then, created a new empty
        gameobject, name it "Spawn" and set it as children of the SceneChanger main prefab. Change the position of the "Spawn
        for setting exaclty where the player will appear when he get's back for this scene. It's generally in front on the
        doors. But be careful not to put it too close for the SceneChanger main prefab BoxCollider2D, because if it happens
        the player will most likely trigger it as soon as he appears in the scene, and we don't want this!
        
        *If there are more than one object that transports you for other same scene, for example, the 4 stairs in the Cin
        that both transports you to the Cin second floor, we must have a way to differ between those objects when getting back
        to the Cin first floor. If it happens, fill the "SpecificName" field naming, for exemple, CinSecondFloor_1, ..., 
        CinSecondFloor_4, and name the prefabs according to this. Otherwise, leave it empty.

        *Please see the PlayerSpawn script for more information.
		
	 */
	public static bool globalLock = false;
	public string changeFX = "DoorSlam";
	public float changeTime = 1.2f;
	public string destinyScene = "";
    public string specificName = "";
	public bool locked = false;

    /// <summary>
    /// Calls the ScreenFader to fade in and then when it's done set's the player to spawn
    /// in the specific position related to the scene he is leaving.
    /// </summary>
	IEnumerator ChangeScene()
	{
        ScreenFader fader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScreenFader>();
		yield return new WaitForSeconds (fader.BeginFade(1, changeTime));

        string sceneName = SceneManager.GetActiveScene().name;
       
        if (specificName != "")
            sceneName = specificName;

		PlayerSpawn.SetTarget(sceneName);
		MusicPlayer.Instance.ChangeSong (destinyScene, changeTime); 
		MusicPlayer.Instance.PlayFX (changeFX); 
		SceneManager.LoadScene (destinyScene);
	}

    public void Change()
    {
		if(!locked && !globalLock)
       		StartCoroutine(ChangeScene());
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.tag == "Player")
            Change();
	}
}
