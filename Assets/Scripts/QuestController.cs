using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class QuestController : MonoBehaviour {
	
	public bool showQuests;
	public GUISkin skin;
	public int numQuests = 0; 
	private int questBookX=400, questBookY=0;
	private List<Quest> questsDatabase;
	private int nextQuestID = 0;
	private int currPage=0;
	private int textBookHeight=200, textBookWidth=230;
	private string description;
	public Quest currQuest;
	
	void Start(){
		this.questsDatabase = new List<Quest>();
		Quest first = new Quest("Entrando no CIn", "Seja Bem-vindo a UFPE!! Corra até o CIn para não perder a aula inaugural!");
		first.questTasks.Add(new Task("Chegar no CIn", "Você deve chegar ao CIn pela porta principal"));
		questsDatabase.Add(first);
		this.numQuests = this.questsDatabase.Count;
		this.showQuests = false;
		this.currQuest = questsDatabase[0];
	}
	
	void Update(){
		if (Input.GetButtonDown("Quests")) this.showQuests = !this.showQuests;
	}
	
	void OnGUI(){
		GUI.skin = skin;
		if (showQuests) {
			DrawQuestBookLeft();
			DrawQuestBookRight();
		}
	}
	
	
	void DrawQuestBookLeft(){
		generateDescription ();
		Rect back = new Rect (questBookX, questBookY + textBookHeight - 30, 29, 29);
		if (back.Contains (Event.current.mousePosition) && Event.current.type == EventType.mouseUp && Event.current.button == 0 && Input.GetMouseButtonUp (0) ) {
			
			this.BackQuest();	
		}
		Rect slotRect = new Rect(questBookX, questBookY, textBookWidth, textBookHeight);
		GUI.BeginGroup(slotRect, skin.GetStyle("PageQuest"));
		GUI.Box(new Rect(0,0,textBookWidth,textBookHeight), this.description, skin.GetStyle("Desc") );
		GUI.EndGroup();
		if (currPage > 0) {
			GUI.Box(back, "<color=#ccc><b> <- " + "  </b></color>", skin.button );
			
		}
	}
	
	void DrawQuestBookRight(){
		int width = questBookX + textBookWidth;
		Rect slotRect = new Rect(questBookX+textBookWidth, questBookY, textBookWidth, textBookHeight);
		GUI.BeginGroup(slotRect,skin.GetStyle("PageQuest"));
		GUI.Box(new Rect(0,0,textBookWidth,textBookHeight), "<color=#ccc><b>Cumpra os seguintes\nobjetivos:</b></color>", skin.GetStyle("Desc") );
		Rect next =  new Rect( 0+textBookWidth -30 , 0+textBookHeight-30, 29, 29);
		if (next.Contains (Event.current.mousePosition) && Event.current.type == EventType.mouseUp && Event.current.button == 0 && Input.GetMouseButtonUp (0)) {
			
			this.NextQuest();
			
		}	

		int var = 50;
		int tasksY = questBookY;
		Item item;
		Task task;
		string desc;
		for(int i=0; i < this.currQuest.questTasks.Count; i++){
			tasksY += var;
			task = this.currQuest.questTasks[i];
			desc = task.taskObjective;
			slotRect = new Rect(width, tasksY, textBookWidth, textBookHeight);
			if(task.taskDone || this.currQuest.questDone) {
				GUI.Box(new Rect(0,tasksY,textBookWidth,textBookHeight), "<color=#ccc>- " + desc +  " (Completo)</color>", skin.GetStyle("Desc") );
				
			}
			else {
				GUI.Box(new Rect(0,tasksY,textBookWidth,textBookHeight), "<color=#ccc>- " + desc +  "</color>", skin.GetStyle("Desc") );
				for(int j=0; j< task.taskItemsToCatch.Count; j++){
					tasksY+=20;
					item = task.taskItemsToCatch[j];
					desc = "<color=#ccc> Colete " + item.itemName;
					GUI.Box(new Rect(0,tasksY,textBookWidth,textBookHeight), desc+  " </color>", skin.GetStyle("Desc") );
					GUI.Box(new Rect(0+5*desc.Length,tasksY+15,28,28), item.itemIcon, skin.GetStyle("Desc"));
					
				}
			}
		}
		if (currPage < numQuests-1) {
			GUI.Box(next, "<color=#ccc><b> -> " + "  </b></color>", skin.button );
		}
		GUI.EndGroup();
		
	}
	
	void generateDescription(){
		this.description = "<color=#ccc><b>" + this.currQuest.questName +" - "+  (this.currQuest.questDone ? "(Completa)" : "(Incompleta)" )   + "</b></color>" + "\n\n";
		this.description += "<color=#ccc>";
		this.description += "Descriçao:\n"+this.currQuest.questDescription;
		this.description+="</color>";
	}
	
	void AddQuest(Quest quest){
		quest.questID = ++this.nextQuestID;
		this.questsDatabase.Add (quest);
		this.numQuests++;
	}
	
	void AddTask(string questName, Task task){
		task.taskID = ++this.nextQuestID;
		Quest quest = this.FindQuest (questName);
		quest.questTasks.Add (task);
	}
	
	void AddTask(Quest quest, Task task){
		task.taskID = ++this.nextQuestID;
		quest.questTasks.Add (task);
	}
	
	void TaskComplete(string questName, string taskName){
		Quest quest = this.FindQuest (questName);
		Task task = this.FindTask (quest, taskName);
		task.taskDone = true;
		quest.questDone = quest.hasAllTasks();
	}
	
	
	bool NextQuest(){
		this.currPage++;
		if (this.currPage >= this.numQuests) {
			this.currPage--; 
			return false;
		}
		this.currQuest = this.questsDatabase[currPage];
		return true; 
		
	}
	
	bool BackQuest(){
		currPage--;
		if (currPage < 0) {
			currPage=0; 
			return false;
		}
		this.currQuest = this.questsDatabase[currPage];
		return true;
	}
	
	Quest FindQuest(string questName){
		for (int i=0; i < this.questsDatabase.Count; i++) {
			if(this.questsDatabase[i].questName.Equals(questName)) {
				return this.questsDatabase[i];
			}
		}
		return null;
	}
	
	Task FindTask(Quest quest, string taskName){
		for (int i=0; i < quest.questTasks.Count; i++) {
			if(quest.questTasks[i].taskName.Equals(taskName)) {
				return quest.questTasks[i];
			}
		}
		return null;
	}
	
	
}




















