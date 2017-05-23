using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// This script is resposible for not allowing camera to get out of bounds of the map by calling
/// 'unfollowPlayer()' on it. It must be attached to a gameobject with a collider2D on it,
///  and unfollow will be called on collider2d trigger. 
/// 
/// Desenvolvido por: Higor
/// </summary>
public class CameraUnfollower : MonoBehaviour
{
    CameraController cameraController;
    public bool unfollowX;
    public bool unfollowY;

	void Start ()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
	}

	/// <summary>
	/// Checks if player is colliding with the object in the corner of the map and unfollowCamera if so
	/// </summary>
	/// <param name="col">The player collider.</param>
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (unfollowX) cameraController.followingPlayerX = false;
            if (unfollowY) cameraController.followingPlayerY = false;
        }
    }

	/// <summary>
	/// Checks if player was colliding with the object in the corner of the map and stop unfollowingCamera if so
	/// </summary>
	/// <param name="col">The player collider</param>
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (unfollowX) cameraController.followingPlayerX = true;
            if (unfollowY) cameraController.followingPlayerY = true;
        }
    }
}
