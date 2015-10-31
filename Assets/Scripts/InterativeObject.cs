using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * InterativeObject é uma classe que permite que
 * os objetos interajam com os "bonecos", essa interação
 * não inclui fala, sendo fala um pacote extra. Esta interação
 * é apenas um indicio de que ele é um objeto que pode ou não
 * possuir animação e pode ou não dar passagem para o PC.
 **/
public class InterativeObject : MonoBehaviour{
	/**Blocked indica se está bloqueado a interação com o objeto**/
	public bool blocked;
	/**neededItens são a lista de itens necessários para que o objeto seja desbloqueado**/
	public List<Item> neededItens;
	/**Sprite para caso se deseja alterar a imagem do objeto**/
	public Sprite response;
	/**changeToOrigins indica se a sprite deve voltar a ser o que era originalmente**/
	public bool changeToOrigins = true;

	/**
	 * Ao entrar em colisão com o objeto
	 * o objeto verifica se o colisor possui
	 * todos os itens necessarios. Em caso afirmativo,
	 * libera a passagem e da uma resposta de animação se tiver
	 **/
	void OnCollisionEnter2D(Collision2D col){
		GameObject obj = col.gameObject;
		bool hasItem = true;
		foreach (Item item in this.neededItens){
			if(!hasItem) break;
			int idItem = obj.GetComponent<InventoryController>().BuscarItem(item.itemName);
			if(idItem == -1) hasItem = false;
		}
		if(hasItem){
			this.blocked = false;
			this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
			if (response != null) {
				Sprite atual = this.response;
				this.response = this.gameObject.GetComponent<SpriteRenderer>().sprite;
				this.gameObject.GetComponent<SpriteRenderer>().sprite = atual;
			}
		}
	}

	/**
	 * Ao Sair do objeto colidido,
	 * o objeto colidido então restaura suas origens
	 * de modo que a passagem tem de ser verificada novamente em cada
	 * caso que o objeto colisor desejar passar pela passagem
	 **/
	void OnTriggerExit2D(Collider2D col){
		this.blocked = true;
		this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
		if (response != null && changeToOrigins) {
			Sprite atual = this.response;
			this.response = this.gameObject.GetComponent<SpriteRenderer>().sprite;
			this.gameObject.GetComponent<SpriteRenderer>().sprite = atual;
		}
	}
}
