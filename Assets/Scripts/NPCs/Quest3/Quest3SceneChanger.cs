using UnityEngine;
using System.Collections;

public class Quest3SceneChanger : MonoBehaviour {

	SceneChanger changer;

	void Start () {
		changer = GetComponent<SceneChanger> ();
	}

	/// <summary>
	/// Check the PreCondition of Cracha.
	/// </summary>
	/// <returns>True if the player inventory has an IDCard.</returns>
	bool Quest3Started()
	{
		User currentUser = User.Instance;
		IPreCondition start = GameManager.Instance.preConditionManager.getPreCondition (14);
		IPreCondition end = GameManager.Instance.preConditionManager.getPreCondition (9);
		return start.checkIfMatches (currentUser) && !end.checkIfMatches (currentUser);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Player") {
			if (Quest3Started())
				changer.destinyScene = "Classroom_3_class";
			else
				changer.destinyScene = "Classroom_3";
			changer.Change ();
		}
			
	}
}
