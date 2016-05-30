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

		//TODO: get values from element.
	
		return preCondition;
	}
}

/* 
 * PreConditionCollection>
 <PreCondition identifier="1" name="CheckCracha" itemIdentifier="1"/>
 <PreCondition identifier="2" name="CheckLogin" itemIdentifier="2"/>
</PreConditionCollection>

*/