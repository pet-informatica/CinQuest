using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;

using UnityEngine;
using System.Collections;

/// <summary>
/// Developed by: Peao (rngs);
/// Quest Builder XML.
/// </summary>
public class QuestBuilderXML
{
	/// <summary>
	/// Developed by: Peao (rngs);
	/// Builds the quest from the Quest XML element.
	/// </summary>
	/// <returns>The quest.</returns>
	/// <param name="quest">Quest.</param>
	public static Quest buildQuest(XElement quest)
	{
		Quest newQuest = null;

		if (quest == null)
			return newQuest;

		if (quest.FirstNode == null)
			return newQuest;

		int identifier = Int32.Parse (quest.Element ("Identifier").Value);
		string name = quest.Element ("Name").Value;
		string description = quest.Element ("Description").Value;
		bool unlocked = quest.Element ("Unlocked").Value == "true" ? true : false;

		XElement preConditionsToUnlock = quest.Element ("PreConditionsToUnlock");
		List<IPreCondition> p1 = new List<IPreCondition> ();
		foreach (XElement element in preConditionsToUnlock.Elements()) {
			int index = Int32.Parse (element.Attribute ("identifier").Value) - 1;
			p1.Add (GameManager.preConditions [index]);
		}

		XElement preConditionsToDone = quest.Element ("PreConditionsToDone");
		List<IPreCondition> p2 = new List<IPreCondition> ();
		foreach (XElement element in preConditionsToDone.Elements()) {
			int index = Int32.Parse (element.Attribute ("identifier").Value) - 1;
			p2.Add (GameManager.preConditions [index]);
		}

		XElement rewards = quest.Element ("Rewards");
		List<Item> r1 = new List<Item> ();
		foreach (XElement element in rewards.Elements()) {
			int index = Int32.Parse (element.Attribute ("identifier").Value) - 1;
			r1.Add (GameManager.items [index]);
		}

		newQuest = new Quest (identifier, name, description, unlocked, p1, p2, r1);
	
		return newQuest;
	}
}

/*  EXAMPLE OF A XML:
 * 
 * <Quest>
		<Identifier>1</Identifier>
		<Name>TestQuest</Name>
		<Description>This is a test quest.</Description>
		<Unlocked>false</Unlocked>
		<PreConditionsToUnlock>
			<PreCondition identifier="1"/>
		</PreConditionsToUnlock>
		<PreConditionsToDone>
			<PreCondition identifier="2"/>
		</PreConditionsToDone>
		<Rewards>
			<Reward identifier="1"/>
		</Rewards>
	</Quest>

 * 
 * */