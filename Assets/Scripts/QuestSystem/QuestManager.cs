using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Peao (rngs);
/// Quest Manager.
/// </summary>
public class QuestManager
{
	private IQuestRepository _questRepository;

	public QuestManager (IQuestRepository questRepository)
	{
		this._questRepository = questRepository;
	}

	/// <summary>
	/// Gets the quests.
	/// </summary>
	/// <returns>The quests.</returns>
	public Dictionary<int, Quest> getQuests(){
		if (this._questRepository.quests.Count < 0) {
			throw new Exception ("No quests were found!");
		}
		return this._questRepository.quests;
	}

	/// <summary>
	/// Loads the quests from repository.
	/// </summary>
	/// <returns><c>true</c>, if quests from repository was loaded, <c>false</c> otherwise.</returns>
	public bool loadQuestsFromFile(string questCollectionFileName){
		this.tryLoadQuests(questCollectionFileName);

		if (this._questRepository.quests.Count > 0)
			return true;
		
		return false;
	}

	/// <summary>
	/// Tries the load quests.
	/// </summary>
	private void tryLoadQuests(string questCollectionFileName){
		this._questRepository.deserialize(questCollectionFileName);
	}
}