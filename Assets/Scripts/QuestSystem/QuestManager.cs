using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Peao (rngs);
/// Quest Manager.
/// </summary>
public class QuestManager
{
	private IQuestRepository questRepository;

	public QuestManager (IQuestRepository questRepository)
	{
		this.questRepository = questRepository;
	}

	/// <summary>
	/// Gets the quests.
	/// </summary>
	/// <returns>The quests.</returns>
	public Dictionary<int, Quest> getQuests(){
		if (this.questRepository.quests.Count < 0) {
			throw new Exception ("No quests were found!");
		}
		return this.questRepository.quests;
	}

	/// <summary>
	/// Loads the quests from repository.
	/// </summary>
	/// <returns><c>true</c>, if quests from repository was loaded, <c>false</c> otherwise.</returns>
	public bool loadQuestsFromRepository(string questCollectionFileName){
		this.tryLoadQuests(questCollectionFileName);

		if (this.questRepository.quests.Count > 0)
			return true;
		
		return false;
	}

	/// <summary>
	/// Tries the load quests.
	/// </summary>
	private void tryLoadQuests(string questCollectionFileName){
		this.questRepository.deserialize(questCollectionFileName);
	}
}