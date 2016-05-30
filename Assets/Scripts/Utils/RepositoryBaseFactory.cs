using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Generic definition of Repository Factory.
/// </summary>
public abstract class RepositoryBaseFactory
{
	public abstract IQuestRepository createQuestRepository();
	public abstract IPreConditionRepository createPreConditionRepository();
	//Others repositories
}