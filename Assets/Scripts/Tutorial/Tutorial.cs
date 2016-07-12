using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Developed by: Peão(rngs);
/// Class that holds the iteration and properties of the Game Tutorial.
/// </summary>
public class Tutorial : MonoBehaviour
{
	/// <summary>
	/// Represents the main Text UI element.
	/// </summary>
	public Text mainText;

	/// <summary>
	/// Gets a value indicating whether this UI <see cref="Tutorial"/> is open.
	/// </summary>
	/// <value><c>true</c> if is open; otherwise, <c>false</c>.</value>
	public bool isOpen { get; private set; }

	/// <summary>
	/// The tutorial Canvas itself.
	/// </summary>
	public GameObject tutorialCanvas;

	/// <summary>
	/// The continue button.
	/// </summary>
	public Button continueBtn;

	/// <summary>
	/// Number of the text frame section.
	/// </summary>
	private int textPosition = 1;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () 
	{
		if (!isOpen && this.checkIfAlertBoxIsOnScene()) {
			this.OpenWindow ();
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update(){
		if (!isOpen && tutorialCanvas.activeSelf) {
			tutorialCanvas.SetActive (false);
		}
	}
		
	/// <summary>
	/// Opens the window.
	/// </summary>
	public void OpenWindow()
	{
		this.tutorialCanvas.SetActive (true);
		isOpen = true;
	}

	/// <summary>
	/// Method that executes when the continue button is pressed.
	/// </summary>
	public void ContinuePressed(){
		if (textPosition < TUTORIAL_MESSAGES.Length && this.checkIfAlertBoxIsOnScene ()) {
			mainText.text = TUTORIAL_MESSAGES [textPosition];
			textPosition += 1;
		} else {
			tutorialCanvas.SetActive (false);
			isOpen = false;
		}
	}

	/// <summary>
	/// Checks if Tutorial is on scene.
	/// </summary>
	/// <returns><c>true</c>, if if tutorialCanvas is on scene, <c>false</c> otherwise.</returns>
	private bool checkIfAlertBoxIsOnScene(){
		if (tutorialCanvas == null)
			throw new NullReferenceException ();
		return true;
	}


	private string[] TUTORIAL_MESSAGES = {"O CInQuest é uma versão jogável do Guia do Aluno do CIn! Nosso objetivo é que você possa aprender sobre o centro jogando!\n\n" +
		"No jogo você terá Quests a realizar que serão associadas com atividades comuns, direitos e obrigações do estudante no CIn!\n\n" +
		"O projeto foi desenvolvido por alunos do PET-Informática e tem como intuito ser uma plataforma Open Source, ou seja, a comunidade poderá colaborar com o código do jogo, dentre outras coisas. Para saber mais sobre o CInQuest fale com a gente através da nossa fanpage e/ou email!" +
		"\n\nwww.fb.com/petinformatica\npetcomputacao-l@cin.ufpe.br\n" ,
		"Aqui alguns comandos e controles que você precisa saber!\n\t\nWASD, Direcionais para movimentação no jogo!\n\nMantenha pressionado o \"LEFT SHIFT\" para correr!\n " +
		"Pressione \"Z\" interagir com os objetos e/ou pessoas.\n" +
		"Pressione \"P\" para acesasr o menu de Puase do jogo.\n" +
		"Pressione \"Q\" para acessar o menu de Quests.\n" +
		"Pressione \"I\" para acessar a mochila de itens.\n" +
		"Pressione \"M\" para maximizar o mapa do jogo.\n",
		"Agora que você já sabe o básico, está pronto e pode explorar o jogo!\nClique em continuar para começar!\n"};
}