using UnityEngine;
using System.Collections;

public class CameraUnfollower : MonoBehaviour
{
    /*
        Desenvolvido por: Higor
        Este script é responsável por impedir que a camera siga o jogador dando
        'unfollowPlayer()' nela. Ele deve ser colocado em um GameObject junto com um
        Collider2D, e é quando o jogador entrar dentro da área deste collider que o script
        dará unfollow na camera. Isto ocorrerá frequentemente quando, por exemplo, o jogador
        chegar aos limites do cenário, sendo necessário travar a câmera para que ele não
        possa ver fora destes limites.
    */

    CameraController cameraController;
    public bool unfollowX;
    public bool unfollowY;

	void Start ()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if (unfollowX) cameraController.followingPlayerX = false;
            if (unfollowY) cameraController.followingPlayerY = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (unfollowX) cameraController.followingPlayerX = true;
            if (unfollowY) cameraController.followingPlayerY = true;
        }
    }
}
