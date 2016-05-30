using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Peao (rngs);
///	Generic definition of a PreCondition Repository. 
/// </summary>
public interface IPreConditionRepository
{
	Dictionary<int, IPreCondition> preConditions { get; }
	bool addPreCondition(IPreCondition newPreCondition);
	bool removePreCondition(int identifier);
	bool updatePreCondition(int identifier, IPreCondition quest);
	IPreCondition searchPreCondition(int identifier);
	void deserialize(string preConditionCollectionFilePath);
}