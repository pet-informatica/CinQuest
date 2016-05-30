using System;

/// <summary>
/// Developed by: Peao (rngs);
/// All the repositories of XML type should have a method to its creation here.
/// </summary>
public class RepositoryXMLFactory : RepositoryBaseFactory
{
	/// <summary>
	/// Creates the quest repository.
	/// </summary>
	/// <returns>The quest repository.</returns>
	public override IQuestRepository createQuestRepository(){
		return new QuestRepositoryXML();
	}

	/// <summary>
	/// Creates the pre condition repository.
	/// </summary>
	/// <returns>The pre condition repository.</returns>
	public override IPreConditionRepository createPreConditionRepository(){
		return new PreConditionRepositoryXML();
	}
}