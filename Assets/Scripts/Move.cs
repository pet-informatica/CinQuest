#define DBG 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/*

    @   Mover eh uma classe que enfilera pontos para um gameobject se deslocar baseando-se em Distancia Manhattan. @ 

			   @  Dever ter @
-Dever ser um Gameobject com Rigidbody2D e Animator

				@  Adicionando o mover a um gameobject  @ 
-Coloque o script do Mover no gameobject desejado
-Va no script de controler do seu gameObject alvo que tera acesso aos metodos do mover 
-Crie um atributo do tipo Mover

   					@ Como configurar o mover @ 
-no Start() do seu script, ou em alguma parte do codigo do seu script, antes de poder usar o Mover, voce pode setalo chamando o metodo: 
     setGameObject(GameObject gameObject, Animator anim, float maxMove_speed)


					@ Como usar @
-para adicionar um ponto a fila chame o metodo: addPoint(Point point, float horizontalFirst )
 Point eh uma classe auxiliar, verificar no projeto.
 horizontalFirst eh uma flag que indica se o gameobject se deslocara primeiro na horizontal para entao se deslocar na vertical ou o contrario.
   true: horizontal primeiro.
   false: vertical primeiro.

-para iniciar o movimento chame o metodo moveToPoints() 
 o personagem ira se deslocar em direncao aos pontos na ordem em que foram enfileirados, se o personagem tiver algum controle pelo teclado, este
 sera desativado, quando o personagem desenfilerar todos os pontos, o controle do teclado eh retomado. \

					@ Exemplo de uso @ 
public class CharacterController : MonoBehaviour {
	public float maxMove_speed = 10f;
	private Animator anim;
	public Mover mover;
	void Start () {
		this.anim = GetComponent<Animator> ();
		this.mover.setGameObject (this.gameObject, this.anim, this.maxMove_speed);
		Point p = new Point (this.transform.position.x + 100, this.transform.position.y + 100);
		this.mover.addPoint ( p, true );
		this.mover.moveToPoints ();
	}
}



				@ Observacao @
EPS para comparacao de float de 5f.
 */

public class Move : MonoBehaviour
{
	public float moveSpeed = 100f;
    public float autoX = 0;
    public float autoY = 0;
	public bool autoControl = false;
	public bool start = false;
	public Queue<Point> pointsToMove = new Queue<Point> ();
	public bool stopInput = false;
    public bool hasPointToGo = false;
    public bool hasCompleted = true;
    public bool completeX = false;
    public bool completeY = false;
	public bool finished = true ;
	float EPS = 5f;

    Animator anim;
    Rigidbody2D rb;
    Point pointToGo;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
		hasPointToGo = false;
	}

	public bool isMoving()
    {
		return this.start && this.hasPointToGo;
	}

	public void addPoint(Point point)
    {
		this.pointsToMove.Enqueue (point);
	}

	public void setAutoControl(float autoX, float autoY)
    {
		this.autoX = autoX;
		this.autoY = autoY;
		this.autoControl = true;
	}

	public void setAutoControl(bool autoControl)
    {
		this.autoControl = autoControl;
	}

	private float fabs(float a, float b)
    {
		a -= b;
		return (a < 0) ? -1 * a : a;
	}

	private bool isAt(Point p, float EPS)
    {
		return (fabs ( p.x , transform.position.x  ) <= EPS ) && ( fabs (p.y , transform.position.y ) <= EPS ) ;
	}

	public void moveToPoints()
    {
		this.finished = false;
		this.start = true;
		hasPointToGo=true;
		hasCompleted=true; 
		completeX=false;
		completeY=false;
		this.stopInput = true;
	}
	
	void Update ()
    {
		if (hasPointToGo)
        {
			if(hasCompleted)
            {
				
				if ( pointsToMove.Count > 0 )
                {
					this.hasCompleted=false;

					pointToGo = pointsToMove.Dequeue();
					this.finished = true;
				}
				else
                {
					this.autoX=this.autoY=0;
					hasPointToGo=false;
					this.stopInput=false;
					this.start=false;
				}
			}		
			else
            {
				float x = pointToGo.x; 
				float y = pointToGo.y;
				
				bool horizontalFirst = pointToGo.horizontalFirst;
				
				this.autoX=this.autoY=0;
				float actualX = transform.position.x, actualY = transform.position.y;

				completeX = fabs(x, actualX) <= EPS;
				completeY = fabs(y, actualY) <= EPS;
				hasCompleted = completeX && completeY ;

				if(!hasCompleted)
                {
					this.autoControl = true;

					if(horizontalFirst)
                    {
						if(!completeX)
                        {
							this.autoX = (x > actualX )? 1 : -1;
						}
						else
                        { 
							this.autoX = 0;
							this.autoY = (y > actualY ) ? 1 : -1;
						}
					}
					else
                    {
						if(!completeY)
                        {
							this.autoY = (y > actualY )? 1 : -1;
						}
						else
                        {
							this.autoY=0;
							this.autoX = (x > actualX ) ? 1 : -1;
						}
					}					
				}

				else this.autoControl=false;
			}
		}
		
		float moveVertical=0;	
		float moveHorizontal=0;

		if (autoControl)
        {
			moveVertical = this.autoY;
			moveHorizontal = this.autoX;
		}

		else if(!this.stopInput)
        {
			//moveVertical = Input.GetAxis("Vertical");
			//moveHorizontal = Input.GetAxis ("Horizontal");
		}
		
		if(!this.stopInput || this.autoControl)
        {
			if (Mathf.Abs (moveVertical) > Mathf.Abs (moveHorizontal))
				moveHorizontal = 0;
			else if (Mathf.Abs (moveHorizontal) > Mathf.Abs (moveVertical))
				moveVertical = 0;
			else
            {
				moveVertical = 0;
				moveHorizontal = 0;
			}

			if(this.anim != null )
            {
				this.anim.SetFloat ("VerticalSpeed",moveVertical);
				this.anim.SetFloat ("HorizontalSpeed", moveHorizontal);
			}

			if(rb != null )
            {
				rb.velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, moveVertical * this.moveSpeed);
				rb.velocity = new Vector2 (moveHorizontal * this.moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
			}
			else
            {
				Vector3 velo = new Vector3( moveHorizontal * this.moveSpeed, moveVertical * this.moveSpeed, 0 );
				transform.position += velo * Time.deltaTime;
			}
		}	
	}
}
