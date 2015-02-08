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
-Arraste o gameobject alvo da Hiearchy para o atributo gameObject desse script ( arrasta do Hierachy para o inspector ) 
-No inspector, no gameobject alvo, arraste o animator para o atributo animator desse script.
-Va no script de controler do seu gameObject alvo que tera acesso aos metodos do mover 
-Crie um atributo do tipo Mover
-Va no hierarchy, arraste o script Mover para o atributo do tipo Mover no script de controle do gameobject alvo que tera acesso a essa instancia

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
public class Mover : MonoBehaviour {

	public float maxMove_speed = 10f;
	public Animator anim;
	public float autoX=0, autoY=0;
	public bool autoControl = false;
	public bool start = false;
	public Queue<Point> pointsToMove = new Queue<Point> ();
	public bool stopInput = false;
	public GameObject gameObject;
	public bool hasPointToGo=false, hasCompleted=true, completeX=false, completeY=false;
	Point pointToGo; 
	public bool finished = true ;
	float EPS = 5f;

	void Start () {
		//this.addPoint ( new Point(100, 100), true ) ;
		//this.moveToPoints ();
		hasPointToGo = false;
	}


	public bool isMoving(){
		return this.start && this.hasPointToGo;

	}

	public Mover(Animator anim, float maxMove_speed, GameObject gameObject){
		this.maxMove_speed = maxMove_speed;
		this.anim = anim;
		this.gameObject = gameObject;
	}
	public Mover(Animator anim, float maxMove_speed){
		this.maxMove_speed = maxMove_speed;
		this.anim = anim;
		this.gameObject = null;
	}
	public Mover(){
		this.gameObject = null;
		this.anim = null;
	}

	public void addPoint(Point point, bool horizontalFirst){
		point.horizontalFirst = horizontalFirst;
		this.pointsToMove.Enqueue (point);
	}


	public void setAutoControl(float autoX, float autoY){
		this.autoX = autoX;
		this.autoY = autoY;
		this.autoControl = true;
	}
	public void setAutoControl(bool autoControl){
		this.autoControl = false;
	}
	public void setGameObject(GameObject gameObject, Animator anim, float maxMove_speed){
		this.gameObject = gameObject;
		this.anim = anim;
		this.maxMove_speed = maxMove_speed;
	}
	private float fabs(float a, float b){
		a -= b;
		return (a < 0) ? -1 * a : a;
	}
	private bool isAt(Point p, float EPS){
		return (fabs ( p.x , transform.position.x  ) <= EPS ) && ( fabs (p.y , transform.position.y ) <= EPS ) ;
	}
	public void moveToPoints(){
		//this.pointsToMove = pointse;

		if (this.gameObject == null) {
			throw new UnityException("Rigidbody2D parameter is not set");
		}

		this.finished = false;
		this.start = true;
		hasPointToGo=true;
		hasCompleted=true; 
		completeX=false;
		completeY=false;
		this.stopInput = true;
#if DBG
		print ("move to points");
#endif
	}
	

	// Update is called once per frame
	void Update () {
#if DBG
		print ("start-> " + start);
#endif

	//	if (!start) return;

		if (hasPointToGo) {
#if DBG
			print ("has point to go");
#endif
			if(hasCompleted ) {
				
				if ( pointsToMove.Count > 0 ) {
					this.hasCompleted=false;

					pointToGo = pointsToMove.Dequeue();
					this.finished = true;
#if DBG
					print ("dequeued");
#endif
				}
				else {
					this.autoX=this.autoY=0;
					hasPointToGo=false;
					this.stopInput=false;
					this.start=false;
				}
			}		
			else {
				float x = pointToGo.x; 
				float y = pointToGo.y;
				
				bool horizontalFirst = pointToGo.horizontalFirst;
				
				this.autoX=this.autoY=0;
				float actualX = transform.position.x, actualY = transform.position.y;

				completeX = fabs(x, actualX) <= EPS;
				completeY = fabs(y, actualY) <= EPS;
				hasCompleted = completeX && completeY ;
				if(!hasCompleted) {
					this.autoControl = true;
					if(horizontalFirst) {
#if DBG
						print ("horizontal first");
#endif
						if(!completeX) {
#if DBG
							print ("setting auto x");
#endif
							this.autoX = (x > actualX )? 1 : -1;
						}
						else{ // move Y now
							this.autoX=0;
							this.autoY = (y > actualY ) ? 1 : -1;
						}
					}
					else {
						if(!completeY) {
							this.autoY = (y > actualY )? 1 : -1;
						}
						else{ // move Y now
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
		if (autoControl) {
			moveVertical = this.autoY;
			moveHorizontal = this.autoX;
		}
		else if(!this.stopInput){
			moveVertical = Input.GetAxis("Vertical");
			moveHorizontal = Input.GetAxis ("Horizontal");
		}
		
		if(!this.stopInput || this.autoControl){
			if (Mathf.Abs (moveVertical) > Mathf.Abs (moveHorizontal))
				moveHorizontal = 0;
			else if (Mathf.Abs (moveHorizontal) > Mathf.Abs (moveVertical))
				moveVertical = 0;
			else {
				moveVertical = 0;
				moveHorizontal = 0;
			}
			if(this.anim != null ) {
				this.anim.SetFloat ("VerticalSpeed",moveVertical);
				this.anim.SetFloat ("HorizontalSpeed", moveHorizontal);
			}

			if(this.gameObject.rigidbody2D != null ){
				this.gameObject.rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveVertical * this.maxMove_speed);
				this.gameObject.rigidbody2D.velocity = new Vector2 (moveHorizontal * this.maxMove_speed, rigidbody2D.velocity.y);
			}
			else{
				Vector3 velo = new Vector3( moveHorizontal * this.maxMove_speed, moveVertical * this.maxMove_speed, 0 );
				this.gameObject.transform.position += velo * Time.deltaTime;
				
			}

		}	
	}
}
