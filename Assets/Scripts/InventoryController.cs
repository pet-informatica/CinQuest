using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {
	public List<Item> inventory = new List<Item>();
	public int slotsX, slotsY;
	public GUISkin skin;

	private bool showInventory;
	private bool showDescription;
	private string description = "";
	private Item dragItem;
	private bool draggingItem;
	private bool overInventory;
	private int dragItemPosition;


	void Start(){
		for (int i = 0; i < this.slotsX*this.slotsY; i++) {
			this.inventory.Add(new Item());
		}
		
		this.showInventory = false;
		this.showDescription = false;
		this.draggingItem = false;
		this.AddItem(new Item ("grape", 0, "Uvinhas para voce", Item.ItemType.Quest));
		this.AddItem(new Item ("grape", 0, "Uvinhas para voce2", Item.ItemType.Consumable));
	}

	void Update(){
		if (Input.GetButtonDown ("Inventory")) {
			this.showInventory = !this.showInventory;
			print (this.showInventory.ToString());
		}
	}

	void OnGUI(){
		GUI.skin = this.skin;
		if (showInventory) {
			DrawInventory();
			if(showDescription && !draggingItem){
				GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 100,100),this.description, skin.GetStyle("Desc"));
			}
		}
		if(draggingItem){
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 35,35),this.dragItem.itemIcon);
		}
		if ((!this.overInventory || !this.showInventory) && this.draggingItem) {
			if(Input.GetMouseButtonUp(0)){
				this.RemoveItem(this.dragItemPosition);
				this.draggingItem = false;
			}
		}
	}

	void DrawInventory(){
		this.overInventory = false;
		for (int i = 0; i < this.slotsY; i++) {
			for(int j = 0 ; j < this.slotsX; j++){
				Rect boxSlot = new Rect(j*40,i*40,40,40);
				GUI.Box(boxSlot,"",skin.GetStyle("Slots"));
				if(boxSlot.Contains(Event.current.mousePosition) && this.draggingItem && Input.GetMouseButtonUp(0)){
					this.inventory[dragItemPosition] = this.inventory[slotsY*i + j];
					this.inventory[slotsY*i + j] = this.dragItem;
					this.draggingItem = false;
				}
				if(this.inventory[slotsY*i + j].itemID != -1){
					GUI.DrawTexture(boxSlot,this.inventory[slotsY*i + j].itemIcon);
					if(boxSlot.Contains(Event.current.mousePosition)){
						this.overInventory = true;
						generateDesc(this.inventory[slotsY*i + j]);
						this.showDescription = true;
						if(Event.current.type == EventType.mouseDrag && Event.current.button == 0 && !this.draggingItem){
							this.draggingItem = true;
							this.dragItem = this.inventory[slotsY*i + j];
							this.dragItemPosition = slotsY*i + j;
							this.inventory[slotsY*i + j] = new Item();
						}
					}
				}else if(boxSlot.Contains(Event.current.mousePosition)){
					this.description = "";
					this.showDescription = false;
					this.overInventory = true;
				}
			}
		}
	}

	private void generateDesc(Item target){
		this.description = "<color=#000000><b>" + target.itemName + "</b></color>" + "\n\n";
		this.description += "<color=#a52a2a>" + target.itemDesc + "</color>";
	}

	public void AddItem(Item item){
		for(int i = 0; i < this.slotsX*this.slotsY; i++){
			if(this.inventory[i].itemID == -1){
				this.inventory[i] = item;
				break;
			}
		}
	}

	public void RemoveItem(int position){
		this.inventory [position] = new Item ();
		print ("Item na posicao " + position + " removido");
	}

	public int BuscarItem(string nome){
		for (int i = 0; i < this.slotsX*this.slotsY; i++) {
			if(this.inventory[i].itemName == nome) return i;
		}
		return -1;
	}

}
