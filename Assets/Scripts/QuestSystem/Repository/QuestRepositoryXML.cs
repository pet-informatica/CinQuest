using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Developed by: Peao (rngs);
/// Quest repository XML.
/// </summary>
public class QuestRepositoryXML : IQuestRepository
{
	private Dictionary<int, Quest> _quests;

	public QuestRepositoryXML ()
	{
		this._quests = new Dictionary<int, Quest> ();
	}

	public Dictionary<int, Quest> quests { 
		get {
			return _quests;
		}
	}

	/// <summary>
	/// Adds the quest.
	/// </summary>
	/// <returns><c>true</c>, if quest was added, <c>false</c> otherwise.</returns>
	/// <param name="newQuest">New quest.</param>
	public bool addQuest(Quest newQuest){
		if (this._quests.ContainsKey (newQuest.identifier))
			return false;
		this._quests.Add(newQuest.identifier, newQuest);
		return true;
	}

	/// <summary>
	/// Removes the quest.
	/// </summary>
	/// <returns><c>true</c>, if quest was removed, <c>false</c> otherwise.</returns>
	/// <param name="identifier">Identifier.</param>
	public bool removeQuest(int identifier){
		return this._quests.Remove (identifier);
	}

	/// <summary>
	/// Updates the quest.
	/// </summary>
	/// <returns><c>true</c>, if quest was updated, <c>false</c> otherwise.</returns>
	/// <param name="identifier">Identifier.</param>
	/// <param name="quest">Quest.</param>
	public bool updateQuest(int identifier, Quest quest) {
		Quest retrievedQuest = this.searchQuest (identifier);
		if (retrievedQuest == null)
			return false;
		retrievedQuest = quest;
		return true;
	}

	/// <summary>
	/// Searchs the quest.
	/// </summary>
	/// <returns>The quest.</returns>
	/// <param name="identifier">Identifier.</param>
	public Quest searchQuest(int identifier){
		Quest ret = null;
		this._quests.TryGetValue(identifier,out ret);
		return ret;
	}

	/// <summary>
	/// Build the Quest Repository by deserealizing the specified questCollectionFileName.
	/// </summary>
	/// <param name="questCollectionFileName">Quest collection file name.</param>
	public void deserialize(string questCollectionFileName){

		TextAsset temp = Resources.Load("QuestCollection") as TextAsset;
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(temp.text);

		XmlNodeReader nodeReader = new XmlNodeReader(doc);
		nodeReader.MoveToContent();
		XDocument xDoc = XDocument.Load(nodeReader);

		if (xDoc != null) {
			foreach (XElement quest in xDoc.Root.Elements()) {
				Quest newQuest = QuestBuilderXML.buildQuest (quest);
				if (newQuest != null)
					this.addQuest (newQuest);
			}
		}
	}
}			