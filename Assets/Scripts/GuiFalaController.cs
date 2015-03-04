using UnityEngine;
using System.Collections;

/**
 * Desenvolvido por: dnr2
 * Usado para mostrar telas de conversa com NPCS.
 * Opcoes como mostrar chaixa de dialogo simples, ou caixa de escolha.
 * So pode trocar o estado de uma conversa apos um certo intervalo de 
 * tempo definido por timeBetweenEvents.
 * Este script deve ser usado em conjunto com outro script que ira manipular
 * quais os estados e o que deve ser falado. 
 */


public class GuiFalaController : MonoBehaviour {
	
	public GUISkin skin;
	public Texture image; //Imagem do NPC
	public int spaceBetweenOptions = 15; //usado para espacamento das opcoes

	private string fala; // Texto principal da caixa de dialogo
	private bool talking;
	private bool question;
	private string[] textOptions;
	private int answerPos;

	//Controle do tempo de transicao entre estados
	private float nextTimeAvailable;	
	public float timeBetweenEvents = 0.1f;


	// Use this for initialization
	void Start () {
		this.talking = false;
		this.question = false;
		nextTimeAvailable = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool canExecuteCommand(){
		return Time.time > nextTimeAvailable;
	}


	//Caixa de dialogo simples
	public void showDialog( string fala ){
		if (canExecuteCommand ()) {
			this.fala = fala;
			this.talking = true;
			nextTimeAvailable = Time.time + timeBetweenEvents;
		}
	}

	//Caixa de dialogo com opcoes de escolha
	public void showQuestionDialog( string fala, string[] textOptions, int optionSlected ){
		if (canExecuteCommand ()) {
			this.fala = fala;
			this.textOptions = textOptions;
			this.talking = true;
			this.question = true;
			answerPos = optionSlected % this.textOptions.Length;
			nextTimeAvailable = Time.time + timeBetweenEvents;
		}
	}

	//setar qual a opcao do questionario deve estar marcada. 
	public void setOptionSlected(int optionSlected){
		if (canExecuteCommand ()) {
			answerPos = optionSlected % this.textOptions.Length;
			nextTimeAvailable = Time.time + timeBetweenEvents;
		}
	}


	public void stopDialog( ){
		if (canExecuteCommand ()) {
			this.talking = false;
			this.question = false;
		}
	}

	void OnGUI(){
		GUI.skin = this.skin;
		if (talking ) {
			Vector3 position = Camera.main.ViewportToScreenPoint (new Vector3(0f,0.75f,0));
			GUI.BeginGroup(new Rect(position.x,position.y,Camera.main.pixelWidth,Camera.main.pixelHeight/4));
			Rect iconRec = new Rect(0f,1f,Camera.main.pixelWidth/6,Camera.main.pixelHeight/4);
			Rect msgRec = new Rect(Camera.main.pixelWidth/6,1f,Camera.main.pixelWidth-Camera.main.pixelWidth/6,Camera.main.pixelHeight/4);
			GUI.Box(iconRec,this.image);
			GUI.Box(msgRec,this.fala,"TextArea");
			GUI.EndGroup ();
			if (this.question ) {
				drawButton();
			}
		}
	}

	private void drawButton(){
		int spacingSum = -50; 
		int currentPosition = 0;
		foreach( string textOption in textOptions){
			int currentSpace = (textOption.Length * 8) + (2 * spaceBetweenOptions);
			GUI.Button(new Rect(Camera.main.pixelWidth/2 + spacingSum,Camera.main.pixelHeight/2, currentSpace ,50), textOption);

			if (answerPos == currentPosition) {
				GUI.DrawTexture(new Rect(Camera.main.pixelWidth/2 + spacingSum,Camera.main.pixelHeight/2-50,50,50), Resources.Load<Texture2D> ("Icons/pointer"));
			}
			spacingSum += currentSpace + spaceBetweenOptions;
			currentPosition++;
		}
	}
}
