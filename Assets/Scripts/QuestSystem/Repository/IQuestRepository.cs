using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Developed by: Peao (rngs);
///	Generic definition of a Quest Repository. 
/// </summary>
public interface IQuestRepository
{
	Dictionary<int, Quest> quests { get; }
	bool addQuest(Quest newQuest);
	bool removeQuest(int identifier);
	bool updateQuest(int identifier, Quest quest);
	Quest searchQuest(int identifier);
	void deserialize(string questCollectionFileName);
}