using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Developed by: Higor (hcmb)
/// Controls all the logic behind the Quest interface. Stores a list of quests to display in the UI panel.
/// </summary>
public class QuestUI : MonoBehaviour
{
    public GameObject questBox;
    public GameObject questInfo;
    public GameObject questBoxContent;
    public GameObject questButton;
    public Text questInfoName;
    public Text questInfoDescription;
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
            Quest quest = new Quest(0, "Roubar comida " + i, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed sagittis fringilla turpis, eget fringilla ex interdum at. Duis non bibendum mauris. Nullam sit amet lectus accumsan, porttitor arcu a, dapibus nulla. Quisque commodo sit amet nisi vel aliquet. Nullam scelerisque, justo quis vulputate gravida, risus lacus tempor eros, a cursus purus ex bibendum mi. Cras non lacinia sem. Morbi sed arcu pellentesque, faucibus arcu quis, tincidunt urna. Maecenas sed neque eu turpis pellentesque rhoncus sodales at nibh. Donec varius, quam sit amet consectetur volutpat, ex risus convallis augue, sit amet dapibus diam lorem ac enim. Nullam ac lacus in augue bibendum elementum quis sed purus." + i, true, null, null, null);
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

    /// <summary>
    /// Close only the QuestInfo Box
    /// </summary>
    public void CloseInfo()
    {
        questInfo.SetActive(false);
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Quests"))
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
        questInfoDescription.text = quest.description;
        questInfo.SetActive(true);
    }
}
