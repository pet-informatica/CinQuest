using System;
using System.Collections.Generic;

public class QuestRepositoryXML : IQuestRepository
{
	private Dictionary<int, Quest> _quests;

	public QuestRepositoryXML ()
	{
		this._quests = new Dictionary<int, Quest> ();
	}

	public Dictionary<int, Quest> quests { 
		get {
			return _quests;
		}
	}

	public bool addQuest(Quest newQuest){
		return this._addQuest (newQuest);
	}

	public bool removeQuest(int identifier){
		return this._quests.Remove (identifier);
	}

	public bool updateQuest(int identifier, Quest quest) {
		Quest retrievedQuest = this.searchQuest (identifier);
		if (retrievedQuest == null)
			return false;
		retrievedQuest = quest;
		return true;
	}

	public Quest searchQuest(int identifier){
		Quest ret = null;
		this._quests.TryGetValue(identifier,out ret);
		return ret;
	}

	public void deserialize(){
		//TODO: Read from XML file all the Quests and initializate the Quests
	}

	private bool _addQuest(Quest newQuest){
		if (this._quests.ContainsKey (newQuest.identifier))
			return false;
		this._quests.Add(newQuest.identifier, newQuest);
		return true;
	}
}

