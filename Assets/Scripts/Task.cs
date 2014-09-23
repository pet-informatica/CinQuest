using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Task {
	
	public string taskName;
	
	public string taskObjective;
	
	public List<Item> taskItemsToCatch;
	
	private List<Item> taskItemsCatched = new List<Item>();
	
	public bool taskDone = false;
	
	public int taskID;
	
	public Task(string taskName, string taskObjective){
		this.taskName = taskName;
		this.taskObjective = taskObjective;
		this.taskItemsToCatch = new List<Item>();		
	}
	
	public Task(string taskName, string taskObjective, List<Item> taskItemsToCatch){
		this.taskName = taskName;
		this.taskObjective = taskObjective;
		this.taskItemsToCatch = taskItemsToCatch;
	}
	
	
	public bool hasAllItens(){
		bool ok;
		for(int i=0; i < this.taskItemsToCatch.Count; i++){
			ok=false;
			for(int j=0; j< this.taskItemsCatched.Count && !ok; j++) ok = this.taskItemsToCatch[i] == this.taskItemsCatched[j];
			if(!ok) return false;
		}
		return true;
	}
	
	public bool hasCatched(int id){
		for(int i=0; i < this.taskItemsCatched.Count; i++){
			if(this.taskItemsCatched[i].itemID == id) return true;
		}
		return false;
	}

	public override bool Equals (object obj)
	{
		return this.taskName.Equals ( ((Task)obj).taskName  ); 
	}
	
	public int getTaskID(){
		return this.taskID;
	}
	
}
