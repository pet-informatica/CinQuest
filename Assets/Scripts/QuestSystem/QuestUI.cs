using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour {

    public GameObject questBox;
    public GameObject questInfo;
    public GameObject questBoxContent;
    public GameObject questButton;
    public Text questInfoName;
    RectTransform questBoxTransform;
    bool opened;
    float questButtonHeight;

    List<Quest> quests = new List<Quest>();
    List<GameObject> buttons = new List<GameObject>();
	
	void Start ()
    {
        questBoxTransform = questBoxContent.GetComponent<RectTransform>();
        questButtonHeight = questButton.GetComponent<RectTransform>().rect.height;

        LoadFakeQuests();
        UpdateQuestBoxContent();

        CloseWindow();
	}

    /// <summary>
    /// Temporary method only for testing purposes. Must be erased latter.
    /// </summary>
    void LoadFakeQuests()
    {
        for(int i = 0; i < 25; ++i)
        {
            Quest quest = new Quest(0, "Roubar comida " + i, "Testando a quest " + i, true, null, null, null);
            quests.Add(quest);
        }
    }

    /// <summary>
    /// Remove all the buttons from QuestManager Box and insert new ones according to the current quests inside quest list.
    /// </summary>
    void UpdateQuestBoxContent()
    {
        foreach (GameObject obj in buttons) Destroy(obj);
        buttons.Clear();

        int buttonAmount = quests.Count;
        questBoxTransform.sizeDelta = new Vector2(0, buttonAmount * questButtonHeight);
        for(int i = 0; i < buttonAmount; ++i)
        {
            GameObject newButton = Instantiate(questButton) as GameObject;
            buttons.Add(newButton);
            RectTransform newButtonTransform = newButton.GetComponent<RectTransform>();
            newButton.transform.SetParent(questBoxContent.transform, false);
            newButtonTransform.anchoredPosition = new Vector2(0, -i * questButtonHeight);
           
            QuestButton buttonScript = newButton.GetComponent<QuestButton>();
            buttonScript.quest = quests[i];
        }

    }

    /// <summary>
    /// Open the Quest Manager Canvas, setting it's gameobject to active
    /// </summary>
    public void OpenWindow()
    {
        questBox.SetActive(true);
        opened = true;
    }

    /// <summary>
    /// Close the Quest Manager Canvas, setting it's gameobject to inactive
    /// </summary>
    public void CloseWindow()
    {
        questBox.SetActive(false);
        questInfo.SetActive(false);
        opened = false;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (opened) CloseWindow();
            else OpenWindow();
        }
	}

    /// <summary>
    /// Change the information in the QuestInfo box for matching a quest
    /// </summary>
    /// <param name="quest">The quest for displaying the info</param>
    public void ChangeSelectedQuest(Quest quest)
    {
        questInfoName.text = quest.name;
        questInfo.SetActive(true);
    }
}
