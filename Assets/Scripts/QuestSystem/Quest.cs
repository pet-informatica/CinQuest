using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Quest
{
	// PRIVATE ATTRIBUTES
	private int _identifier { get; }
	private string _name { get; }
	private string _description { get; }
	private bool _unlocked { get; set; }
	private bool _done { get; }
	private List<IPreCondition> _preConditionsToUnlock { get; }
	private List<IPreCondition> _preConditionsToDone { get; }
	private List<Reward> _rewards;

	// PUBLIC PROPERTIES
	public int identifier { get { return this._identifier; } }
	public string name { get { return this._name; } }
	public string description { get { return this._description; } }
	public bool unlocked { get { return this.unlocked; } }
	public bool done { get { return this.done; } }
	public List<IPreCondition> preConditionsToUnlock { get { return this._preConditionsToUnlock; } }
	public List<IPreCondition> preConditionsToDone { get { return this._preConditionsToDone; } }

	public Quest (int identifier, string name, string description, bool unlocked, List<IPreCondition> preConditionsToUnlock, List<IPreCondition> preConditionsToDone, List<Reward> rewards)
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

	public List<Reward> getRewards(User currentUserProfile){
		if (this.checkPreConditionsStatus (currentUserProfile, _preConditionsToDone))
			return this._rewards;
		else
			return null;
	}

	public bool activate(User currentUserProfile){
		if (this.checkPreConditionsStatus (currentUserProfile, _preConditionsToUnlock)) {
			this._unlocked = true;
			return true;
		}
		return false;
	}

	// If some pre condition does not matches then return false
	private bool checkPreConditionsStatus(User currentUserProfile, List<IPreCondition> preConditions){
		foreach (IPreCondition p in preConditions){
			if (!p.checkIfMatches(currentUserProfile))
				return false;
		}
		return true;
	}		
}