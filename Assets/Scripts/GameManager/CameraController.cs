using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Desenvolvido por: Higor
/// Este script tem o controle sobre o movimento da câmera, fazendo com que
/// ela siga o PC de forma suave. Caso 'followingPlayer' seja setado para falso,
/// a camera para de seguir o jogador. Isso geralmente acontecerá quando o
/// jogador chegar aos limites dos mapas, sendo necessário parar a câmera para
/// que ele não consiga ver fora destes limites.
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
	/// Following the player 
	/// </summary>
	void TrackPlayer(){
		followingPlayerX = true;
		followingPlayerY = true;
		if(GameObject.FindGameObjectWithTag("Player") != null)
			player = GameObject.FindGameObjectWithTag("Player").transform;
	}
		
	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
		TrackPlayer ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	/// <summary>
	/// Raises the level finished loading event.
	/// </summary>
	/// <param name="scene">Scene.</param>
	/// <param name="mode">Mode.</param>
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
