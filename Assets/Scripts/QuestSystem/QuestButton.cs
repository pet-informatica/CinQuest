using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestButton : MonoBehaviour {

    public Quest quest;

    QuestUI questUI;

    void Start()
    {
        questUI = GameObject.Find("QuestManager").GetComponent<QuestUI>();
        GetComponentInChildren<Text>().text = quest.name;
    }   

    public void OnClick()
    {
        questUI.ChangeSelectedQuest(quest);
    }
}
