using System;

/// <summary>
/// Developed by: Peao (rngs);
/// Generic pre condition.
/// </summary>
public class GenericPreCondition : IPreCondition {

	public int identifier { get; set;}
	public string name { get; set; }
	public int itemIdentifier { get; set; }

	public GenericPreCondition (int identifier, string name, int itemIdentifier)
	{
		this.identifier = identifier;
		this.name = name;
		this.itemIdentifier = itemIdentifier;
	}

	public bool checkIfMatches(User userProfile){
		foreach (GenericItem i in userProfile.Items) {
			if (i.Identifier.Equals(this.itemIdentifier))
				return true;
		}
		return false;
	}
}

/* 
 * <PreCondition identifier="1" name="CheckCracha" itemIdentifier="1"/>
 * <PreCondition identifier="2" name="CheckLogin" itemIdentifier="2"/>
 * */