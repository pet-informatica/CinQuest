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
    const float EPS = 5f;
    bool canMove;

    Animator anim;
    Rigidbody2D rb;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}

	public bool isMoving()
    {
        return this.canMove && this.path.Count > 0;
	}

	public void addPoint(Vector2 point)
    {
		this.path.Enqueue (point);
	}

	private float fabs(float a, float b)
    {
		a -= b;
		return (a < 0) ? -1 * a : a;
	}

	private bool isAt(Vector2 pos)
    {
		return (fabs ( pos.x , transform.position.x  ) <= EPS ) && ( fabs (pos.y , transform.position.y ) <= EPS ) ;
	}

    public void StartMoving()
    {
        this.canMove = true;
	}

    public void StopMoving()
    {
        this.canMove = false;
        this.anim.SetFloat("VerticalSpeed", 0);
        this.anim.SetFloat("HorizontalSpeed", 0);
    }

   
	
	void Update ()
    {
        if (path.Count == 0)
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
