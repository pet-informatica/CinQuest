using System;

/*
 *  Developed by Peao (rngs);
 * 
 * 	Generic definition of Repository Factory.
 * 
 * */

public abstract class RepositoryBaseFactory
{
	public abstract IQuestRepository createQuestRepository();
	//Others repositories
}