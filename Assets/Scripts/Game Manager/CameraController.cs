using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    /*
        Desenvolvido por: Higor
        Este script tem o controle sobre o movimento da câmera, fazendo com que
        ela siga o PC de forma suave. Caso 'followingPlayer' seja setado para falso,
        a camera para de seguir o jogador. Isso geralmente acontecerá quando o
        jogador chegar aos limites dos mapas, sendo necessário parar a câmera para
        que ele não consiga ver fora destes limites.
    */

    public bool followingPlayerX;
    public bool followingPlayerY;
    public float followSpeed = 7.5f;
    Transform player;

	void Start ()
    {
        followingPlayerX = true;
        followingPlayerY = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
    {
        Vector3 target = transform.position;
        if(followingPlayerX) target.x = player.transform.position.x;
        if(followingPlayerY) target.y = player.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
	}
}
