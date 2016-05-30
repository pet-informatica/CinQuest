using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Peao (rngs);
/// Pre condition manager.
/// </summary>
public class PreConditionManager
{
	private IPreConditionRepository _preConditionRepository;

	public PreConditionManager (IPreConditionRepository preConditionRepository)
	{
		this._preConditionRepository = preConditionRepository;	
	}

	/// <summary>
	/// Gets the pre conditions.
	/// </summary>
	/// <returns>The pre conditions.</returns>
	public Dictionary<int, IPreCondition> getPreConditions(){
		if(this._preConditionRepository.preConditions.Count < 0)
			throw new Exception ("Error: no repository of preConditions is empty."); 
		return this._preConditionRepository.preConditions;
	}

	public bool loadPreConditionsFromFile(string preConditionCollectionFilePath){
		this.tryLoadPreConditions (preConditionCollectionFilePath);

		if (this._preConditionRepository.preConditions.Count > 0)
			return true;

		return false;
	}

	/// <summary>
	/// Tries the load the pre conditions.
	/// </summary>
	/// <param name="preConditionCollectionFilePath">Pre condition collection file path.</param>
	private void tryLoadPreConditions(string preConditionCollectionFilePath){
		this._preConditionRepository.deserialize (preConditionCollectionFilePath);
	}
}
