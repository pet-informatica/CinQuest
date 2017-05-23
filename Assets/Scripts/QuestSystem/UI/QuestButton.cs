using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Button that shows a quest information
/// </summary>
public class QuestButton : MonoBehaviour {

    public Quest quest;

    QuestUI questUI;

    void Start()
    {
        questUI = GameObject.Find("QuestManager").GetComponent<QuestUI>();
        GetComponentInChildren<Text>().text = quest.Name;
    }   

	/// <summary>
	/// After click on a quest, a new window with the quest informations appears 
	/// </summary>
    public void OnClick()
    {
        questUI.ChangeSelectedQuest(quest);
    }
}
