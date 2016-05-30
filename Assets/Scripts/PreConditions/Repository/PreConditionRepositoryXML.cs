using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// Developed by: Peao (rngs);
/// Pre condition repository XM.
/// </summary>
public class PreConditionRepositoryXML : IPreConditionRepository
{
	private Dictionary<int, IPreCondition> _preConditions;

	public PreConditionRepositoryXML ()
	{
		this._preConditions = new Dictionary<int, IPreCondition> ();
	}

	public Dictionary<int, IPreCondition> preConditions {
		get {
			return this._preConditions;
		}
	}

	/// <summary>
	/// Adds the pre condition.
	/// </summary>
	/// <returns><c>true</c>, if pre condition was added, <c>false</c> otherwise.</returns>
	/// <param name="newPreCondition">New pre condition.</param>
	public bool addPreCondition(IPreCondition newPreCondition){
		if (this._preConditions.ContainsKey (newPreCondition.identifier))
			return false;
		this._preConditions.Add (newPreCondition.identifier, newPreCondition);
		return true;
	}

	/// <summary>
	/// Removes the pre condition.
	/// </summary>
	/// <returns><c>true</c>, if pre condition was removed, <c>false</c> otherwise.</returns>
	/// <param name="identifier">Identifier.</param>
	public bool removePreCondition(int identifier){
		return this._preConditions.Remove (identifier);
	}

	/// <summary>
	/// Updates the pre condition.
	/// </summary>
	/// <returns><c>true</c>, if pre condition was updated, <c>false</c> otherwise.</returns>
	/// <param name="identifier">Identifier.</param>
	/// <param name="preCondition">Pre condition.</param>
	public bool updatePreCondition(int identifier, IPreCondition preCondition){
		IPreCondition retrievedPreCondition = this.searchPreCondition (identifier);
		if (retrievedPreCondition == null)
			return false;
		retrievedPreCondition = preCondition;
		return true;
	}

	/// <summary>
	/// Searchs the pre condition.
	/// </summary>
	/// <returns>The pre condition.</returns>
	/// <param name="identifier">Identifier.</param>
	public IPreCondition searchPreCondition(int identifier){
		IPreCondition ret = null;
		this._preConditions.TryGetValue (identifier, out ret);
		return ret;
	}

	/// <summary>
	/// Deserialize the specified preConditonCollectionFileName.
	/// </summary>
	/// <param name="preConditonCollectionFileName">Pre conditon collection file name.</param>
	public void deserialize(string preConditonCollectionFilePath){
		XDocument doc = XDocument.Load(preConditonCollectionFilePath);

		if (doc != null) {
			foreach (XElement preCondition in doc.Root.Elements()) {
				IPreCondition newPreCondition = PreConditionBuilderXML.buildPreCondition (preCondition);
				if (newPreCondition != null)
					this.addPreCondition (newPreCondition);
			}
		}
	}

}

