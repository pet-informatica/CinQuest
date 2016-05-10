using System;
using System.Collections.Generic;

public class QuestManager
{
	private IQuestRepository questRepository;

	public QuestManager (IQuestRepository questRepository)
	{
		this.questRepository = questRepository;
	}

	public Dictionary<int, Quest> getQuests(){
		if (this.questRepository.quests.Count < 0) {
			throw new Exception ("No quests were found!");
		}
		return this.questRepository.quests;
	}

	public bool loadQuestsFromRepository(){
		this.tryLoadQuests();

		if (this.questRepository.quests.Count > 0)
			return true;
		
		return false;
	}

	private void tryLoadQuests(){
		this.questRepository.deserialize();
	}
}