using System;
using System.Xml.Linq;

/// <summary>
/// Developed by: Peao (rngs);
/// Pre conditon builder XML.
/// </summary>
public class PreConditionBuilderXML
{
	public static IPreCondition buildPreCondition(XElement element){
		GenericPreCondition preCondition = null;

		if (element == null)
			return preCondition;

		if (!element.HasAttributes)
			return preCondition;

		int identifier = Int32.Parse (element.Attribute ("identifier").Value) - 1;
		string name = element.Attribute ("name").Value;
		int itemIdentifier = Int32.Parse (element.Attribute ("itemIdentifier").Value);

		preCondition = new GenericPreCondition (identifier, name, itemIdentifier);

		return preCondition;
	}
}

/* 
 * PreConditionCollection>
 <PreCondition identifier="1" name="CheckCracha" itemIdentifier="1"/>
 <PreCondition identifier="2" name="CheckLogin" itemIdentifier="2"/>
</PreConditionCollection>

*/