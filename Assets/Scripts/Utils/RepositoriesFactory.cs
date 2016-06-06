using System;

public class RepositoriesFactory
{
	/// <summary>
	/// Developed by: Peao (rngs);
	/// Method to instantiate the QuestRepository based on DatabaseStorageType.
	/// </summary>
	public static IQuestRepository createQuestRepository(EDatabaseStorageType type){
		switch (type) {
		case EDatabaseStorageType.XML:
			return new RepositoryXMLFactory().createQuestRepository();
		default:
			return new RepositoryXMLFactory().createQuestRepository();
		}
	}

    /// <summary>
	/// Developed by: Higor (hcmb);
	/// Method to instantiate the ItemRepository based on DatabaseStorageType.
	/// </summary>
	public static IGenericItemRepository createItemRepository(EDatabaseStorageType type)
    {
        switch (type)
        {
            default:
                return new GenericItemRepository();
        }
    }

    /// <summary>
    /// Developed by: Peao (rngs);
    /// Method to instantiate the PreConditionRepository based on DatabaseStorageType.
    /// </summary>
    public static IPreConditionRepository createPreConditionRepository(EDatabaseStorageType type){
		switch (type) {
		case EDatabaseStorageType.XML:
			return new RepositoryXMLFactory().createPreConditionRepository();
		default:
			return new RepositoryXMLFactory().createPreConditionRepository();
		}
	}
}

