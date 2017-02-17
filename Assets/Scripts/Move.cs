using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/*

    @   Mover eh uma classe que enfilera pontos para um gameobject se deslocar baseando-se em Distancia Manhattan. @ 

			   @  Dever ter @
-Dever estar acoplada a um Gameobject com um Animator configurado com os parâmetros padrões HorizontalSpeed e VerticalSpeed

				@  Adicionando o mover a um gameobject  @ 
-Coloque o script do Mover no gameobject desejado
-Va no script de controler do seu gameObject alvo que tera acesso aos metodos do mover 
-Crie um atributo do tipo Mover

					@ Como usar @
-para adicionar um ponto a fila chame o metodo: addPoint(Vector2 point)
-para iniciar o movimento chame o metodo StartMove() 
 o personagem ira se deslocar em direncao aos pontos na ordem em que foram enfileirados

					@ Exemplo de uso @ 
public class CharacterController : MonoBehaviour {
	public Mover mover;
	void Start () {
		Vector2 p = new Vector2 (this.transform.position.x + 100, this.transform.position.y + 100);
		this.mover.addPoint ( p );
		this.mover.moveToPoints ();
	}
}

				@ Observacao @
EPS para comparacao de float de 5f.
 */

public class Move : MonoBehaviour
{
	public float moveSpeed = 100f;
    public Queue<Vector2> path = new Queue<Vector2>();
	protected const float EPS = 5f;
	protected bool canMove;
	protected bool playerHit;

    protected Animator anim;
	protected Rigidbody2D rb;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

	public bool isMoving()
    {
        return this.canMove && this.path.Count > 0;
	}

	/// <summary>
	/// Adds a waypoint to the path queue
	/// </summary>
	/// <param name="point">Point.</param>
	public void addPoint(Vector2 point)
    {
		this.path.Enqueue (point);
	}

	private float fabs(float a, float b)
    {
		a -= b;
		return (a < 0) ? -1 * a : a;
	}

	/// <summary>
	/// Checks if player is at current position ignoring double precision mistakes
	/// </summary>
	/// <returns>The <see cref="System.Boolean"/>.</returns>
	/// <param name="pos">Position.</param>
	private bool isAt(Vector2 pos)
    {
		return (fabs ( pos.x , transform.position.x  ) <= EPS ) && ( fabs (pos.y , transform.position.y ) <= EPS ) ;
	}

	/// <summary>
	/// Starts or restarts the movement in the current assigned path queue
	/// </summary>
    public void StartMoving()
    {
        this.canMove = true;
		this.anim.enabled = true;
	}

	/// <summary>
	/// Pauses the movement, but can be started latter on.
	/// </summary>
    public void StopMoving()
    {
        this.canMove = false;
        this.anim.SetFloat("VerticalSpeed", 0);
        this.anim.SetFloat("HorizontalSpeed", 0);
		this.anim.Update (Time.deltaTime);
		this.anim.enabled = false;
    }

	/// <summary>
	/// Clears the waypoint queue, halting the character. It won't be able to start moving again in the same
	/// path
	/// </summary>
	public void CancelPath(){
		this.path.Clear ();
	}

	/// <summary>
	/// Checks if the player is in the way of the character by analising player position and character direction speed
	/// </summary>
	/// <returns><c>true</c>, if player is blocking character path, <c>false</c> otherwise.</returns>
	protected bool PlayerBlockingPath(Vector3 player){
		return (
			(player.y > transform.position.y && this.anim.GetFloat("VerticalSpeed") > 0) ||
			(player.y < transform.position.y && this.anim.GetFloat("VerticalSpeed") < 0 ) ||
			(player.x > transform.position.x && this.anim.GetFloat("HorizontalSpeed") > 0 ) ||
			(player.x < transform.position.x && this.anim.GetFloat("HorizontalSpeed") < 0 )
		);
	}

	/// <summary>
	/// Checks if the character collided with the player, and, if so, stop moving
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionStay2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (isMoving () && PlayerBlockingPath(col.transform.position)) {
				StopMoving ();
				playerHit = true;
			}
		}
	}

	/// <summary>
	/// Check if the character was moving before colliding with the player
	/// and, if so, start moving it again
	/// </summary>
	/// <param name="col">Col.</param>
	void OnCollisionExit2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (playerHit) {
				StartMoving ();
				playerHit = false;
			}
		}
	}

	/// <summary>
	/// Check if the character is in a moving state, and, if so, pops the next waypoint from the queue
	/// and move it to it.
	/// </summary>
	void Update ()
    {
		if (path.Count == 0 && canMove)
            StopMoving();

        if (canMove && path.Count > 0)
        {
            Vector2 current = path.Peek();
            if(this.isAt(current))
            {
                path.Dequeue();
            }
            else
            {
                float x = 0;
                float y = 0;

                if (fabs(transform.position.x, current.x) >= EPS)
                    x = transform.position.x < current.x ? 1 : -1;
                else
                    y = transform.position.y < current.y ? 1 : -1;

                if (rb != null)
                    rb.velocity = new Vector2(x * Time.deltaTime * moveSpeed, y * Time.deltaTime * moveSpeed);
                else
                    transform.position += new Vector3(x * Time.deltaTime * moveSpeed, y * Time.deltaTime * moveSpeed, 0);
                   

				if(anim != null)
                {
                    this.anim.SetFloat("VerticalSpeed", y);
                    this.anim.SetFloat("HorizontalSpeed", x);
                }      
            }
        }
	}
}
