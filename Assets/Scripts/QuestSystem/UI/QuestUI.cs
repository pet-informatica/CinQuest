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
	public Text questStatus;
	RectTransform questBoxTransform;
	bool opened;
	float questButtonHeight;
	private MenuStatus menuStatus;

	List<Quest> quests = new List<Quest>();
	List<GameObject> buttons = new List<GameObject>();

	void Start ()
	{
		questBoxTransform = questBoxContent.GetComponent<RectTransform>();
		questButtonHeight = questButton.GetComponent<RectTransform>().rect.height;
		menuStatus = GameManager.Instance.menuStatus;

		UpdateQuestsFromUser();

		CloseWindow();
	}

	/// <summary>
	/// Get all the current quests from the user instance updated.
	/// </summary>
	public void UpdateQuestsFromUser()
	{
		quests.Clear();
		User user = User.Instance;
		foreach(Quest quest in user.Quests.Values)
		{
			quests.Add(quest);
		}
		UpdateQuestBoxContent();
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
		int offset = 0;
		for(int i = 0; i < buttonAmount; ++i)
		{
			//if (!quests[i].Unlocked)
			//	continue;

			GameObject newButton = Instantiate(questButton) as GameObject;
			buttons.Add(newButton);
			RectTransform newButtonTransform = newButton.GetComponent<RectTransform>();
			newButton.transform.SetParent(questBoxContent.transform, false);
			newButtonTransform.anchoredPosition = new Vector2(0, -offset * questButtonHeight);

			QuestButton buttonScript = newButton.GetComponent<QuestButton>();
			buttonScript.quest = quests[i];
			offset++;
		}
	}

	/// <summary>
	/// Open the Quest Manager Canvas, setting it's gameobject to active
	/// </summary>
	public void OpenWindow()
	{
		UpdateQuestBoxContent();
		questBox.SetActive(true);
		opened = true;
		menuStatus.open ("Quest");
	}

	/// <summary>
	/// Close the Quest Manager Canvas, setting it's gameobject to inactive
	/// </summary>
	public void CloseWindow()
	{
		questBox.SetActive(false);
		questInfo.SetActive(false);
		opened = false;
		menuStatus.close ("Quest");
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
		if (Input.GetButtonDown("Quests") && !menuStatus.openProblem("Quest"))
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
		questInfoName.text = quest.Name;
		questInfoDescription.text = quest.Description;

		if (!quest.Unlocked) {
			questStatus.text = GameConstants.QUEST_UNLOCKED;
			questStatus.color = Color.red;
		} else if (quest.Done) {
			questStatus.text = GameConstants.QUEST_COMPLETED;
			questStatus.color = Color.yellow;
		} else {
			questStatus.text = GameConstants.QUEST_NOT_COMPLETED;
			questStatus.color = Color.white;
		}

		questInfo.SetActive(true);
	}
}