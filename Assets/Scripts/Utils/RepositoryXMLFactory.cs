using System;

/*
 *  Developed by Peao (rngs);
 * 
 * 	All the repositories of XML type should have a method to its creation here.
 * 
 * */

public class RepositoryXMLFactory : RepositoryBaseFactory
{
	
	public override IQuestRepository createQuestRepository(){
		return new QuestRepositoryXML();
	}
}