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
        public int rewardGold;
        public int rewardExp;
        public bool completed;
        public string requiredBuildingName;
    }

    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questConditionText;
    public TextMeshProUGUI questRewardText;
    public Button claimButton;

    public List<Quest> quests;
    public int currentQuestIndex;

    public GameManager gameManager;
    public ExpSystem expSystem;

    public AudioSource Claim;

    private void Start()
    {
        quests = new List<Quest>
        {
            new Quest { name = "My house", condition = "Buy a first building", rewardGold = 200, rewardExp = 50},
            new Quest { name = "First money", condition = "Collect 300 coins", rewardGold = 100, rewardExp = 100},
            new Quest { name = "Build", condition = "Buy second buildings", rewardGold = 200, rewardExp = 500},
            new Quest { name = "Green and white", condition = "Build a house with a green roof", rewardGold = 200, rewardExp = 500, requiredBuildingName = "Building3"},
            new Quest { name = "There's no limit to the money", condition = "Buy a fifth building", rewardGold = 200, rewardExp = 650, requiredBuildingName = "Building5" }
        };

        currentQuestIndex = 0;
        UpdateUI();
    }

    private void Update()
    {
        CheckQuestCompletion();
        expSystem.CheckForLevelUp();
        expSystem.UpdateInterface();
    }

    private void CheckQuestCompletion()
    {
        if (currentQuestIndex < quests.Count && !quests[currentQuestIndex].completed)
        {
            if (currentQuestIndex == 0 && gameManager.buildingPlaced != null )
            {
                // якщо умова першого квесту виконана, зробити кнопку ≥нтерактивною
                claimButton.interactable = true;
            }
            else if (currentQuestIndex == 1 && gameManager.coin >= 300)
            {
                // якщо умова другого квесту виконана, зробити кнопку ≥нтерактивною
                claimButton.interactable = true;
            }
            else if (currentQuestIndex == 2 && gameManager.buildingsBought >= 2)
            {
                // якщо умова третього квесту виконана, зробити кнопку ≥нтерактивною
                claimButton.interactable = true;
            }
            else if (currentQuestIndex == 3 && gameManager.buildingPlaced != null && gameManager.buildingPlaced.name == quests[currentQuestIndex].requiredBuildingName)
            {
                // якщо умова четвертого квесту виконана, зробити кнопку ≥нтерактивною
                claimButton.interactable = true;
            }
            else if (currentQuestIndex == 4 && gameManager.buildingPlaced != null && gameManager.buildingPlaced.name == quests[currentQuestIndex].requiredBuildingName)
            {
                // якщо умова четвертого квесту виконана, зробити кнопку ≥нтерактивною
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
        questRewardText.text = "+" + currentQuest.rewardGold + " coins\n" + "+" + currentQuest.rewardExp + " exp";
        claimButton.interactable = false; // «аблокувати кнопку до виконанн€ умови
    }

    public void ClaimReward()
    {
        if (currentQuestIndex < quests.Count && !quests[currentQuestIndex].completed)
        {
            gameManager.coin += quests[currentQuestIndex].rewardGold;
            expSystem.totalExperience += quests[currentQuestIndex].rewardExp;
            quests[currentQuestIndex].completed = true; // ѕозначити квест €к вже забраний
            Claim.Play();
            currentQuestIndex++;

            if (currentQuestIndex < quests.Count)
            {
                UpdateUI();
            }
            else
            {
                // якщо б≥льше немаЇ квест≥в
                questNameText.text = "Oops...";
                questConditionText.text = "You have completed all quests";
                questRewardText.text = "";
                claimButton.gameObject.SetActive(false);
            }
        }
    }
}

