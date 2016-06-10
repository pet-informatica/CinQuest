using System;
using System.Collections.Generic;

public class User
{

	public string name { get; set; }
	public Dictionary<int,Quest> userQuests { get; set;}
	public List<GenericItem> items { get; set;} 

	public User (string name)
	{
		this.name = name;
		this.userQuests = new Dictionary<int,Quest> ();
		this.items = new List<GenericItem>();
	}
		
}