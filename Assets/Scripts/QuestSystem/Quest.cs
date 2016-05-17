using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Developed by: Peao (rngs);
/// Class that represents a Quest.
/// </summary>
public class Quest
{
	// PRIVATE ATTRIBUTES
	private int _identifier { get; set; }
	private string _name { get; set;}
	private string _description { get; set;}
	private bool _unlocked { get; set; }
	private bool _done { get; set;}
	private List<IPreCondition> _preConditionsToUnlock { get; set;}
	private List<IPreCondition> _preConditionsToDone { get; set;}
	private List<Item> _rewards;

	// PUBLIC PROPERTIES
	public int identifier { get { return this._identifier; } }
	public string name { get { return this._name; } }
	public string description { get { return this._description; } }
	public bool unlocked { get { return this.unlocked; } }
	public bool done { get { return this.done; } }
	public List<IPreCondition> preConditionsToUnlock { get { return this._preConditionsToUnlock; } }
	public List<IPreCondition> preConditionsToDone { get { return this._preConditionsToDone; } }

	public Quest() {}

	public Quest (int identifier, string name, string description, bool unlocked, List<IPreCondition> preConditionsToUnlock, List<IPreCondition> preConditionsToDone, List<Item> rewards)
	{
		this._identifier = identifier;
		this._name = name;
		this._description = description;
		this._unlocked = unlocked;
		this._done = false;
		this._preConditionsToUnlock = preConditionsToUnlock;
		this._preConditionsToDone = preConditionsToDone;
		this._rewards = rewards;
	}

	public List<Item> getRewards(User currentUserProfile){
		if (this.checkPreConditionsStatus (currentUserProfile, _preConditionsToDone))
			return this._rewards;
		else
			return null;
	}

	/// <summary>
	/// Tries to activate the Quest based on currentUserProfile.
	/// </summary>
	/// <param name="currentUserProfile">Current user profile.</param>
	public bool activate(User currentUserProfile){
		if (this.checkPreConditionsStatus (currentUserProfile, _preConditionsToUnlock)) {
			this._unlocked = true;
			return true;
		}
		return false;
	}
		
	// <summary>
	/// Checks the pre conditions status.
	/// </summary>
	/// <returns><c>true</c>, if pre conditions status was checked, <c>false</c> otherwise.</returns>
	/// <param name="currentUserProfile">Current user profile.</param>
	/// <param name="preConditions">Pre conditions.</param>/
	private bool checkPreConditionsStatus(User currentUserProfile, List<IPreCondition> preConditions){
		foreach (IPreCondition p in preConditions){
			if (!p.checkIfMatches(currentUserProfile))
				return false;
		}
		return true;
	}		
}