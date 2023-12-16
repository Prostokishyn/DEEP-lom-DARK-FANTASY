using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public string name;
        public string condition;
        public int reward;
        public bool completed;
    }

    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questConditionText;
    public TextMeshProUGUI questRewardText;
    public Button claimButton;

    private List<Quest> quests;
    private int currentQuestIndex;

    public GameManager gameManager;

    private void Start()
    {
        quests = new List<Quest>
        {
            new Quest { name = "Money", condition = "Collect 10 coins", reward = 100 },
            new Quest { name = "My house", condition = "Buy a first building", reward = 200 }
        };

        currentQuestIndex = 0;
        UpdateUI();
    }

    private void Update()
    {
        CheckQuestCompletion();
    }

    private void CheckQuestCompletion()
    {
        if (currentQuestIndex < quests.Count && !quests[currentQuestIndex].completed)
        {
            if (currentQuestIndex == 0 && gameManager.coin >= 10)
            {
                // якщо умова першого квесту виконана, зробити кнопку ≥нтерактивною
                claimButton.interactable = true;
            }
            else if (currentQuestIndex == 1 && gameManager.buildingPlaced != null)
            {
                // якщо умова другого квесту виконана, зробити кнопку ≥нтерактивною
                claimButton.interactable = true;
            }
            else
            {
                // ≤накше заблокувати кнопку
                claimButton.interactable = false;
            }
        }
    }

    private void UpdateUI()
    {
        Quest currentQuest = quests[currentQuestIndex];
        questNameText.text = currentQuest.name;
        questConditionText.text = currentQuest.condition;
        questRewardText.text = "+" + currentQuest.reward + " coins";
        claimButton.interactable = false; // «аблокувати кнопку до виконанн€ умови
    }

    public void ClaimReward()
    {
        if (currentQuestIndex < quests.Count && !quests[currentQuestIndex].completed)
        {
            // якщо кнопка натиснута та квест не забраний, отримати нагороду
            gameManager.coin += quests[currentQuestIndex].reward;
            quests[currentQuestIndex].completed = true; // ѕозначити квест €к вже забраний

            currentQuestIndex++;

            if (currentQuestIndex < quests.Count)
            {
                UpdateUI();
            }
            else
            {
                // якщо б≥льше немаЇ квест≥в
                questNameText.text = "No more quests";
                questConditionText.text = "You have completed all the quests!";
                questRewardText.text = "";
                claimButton.gameObject.SetActive(false);
            }
        }
    }
}

