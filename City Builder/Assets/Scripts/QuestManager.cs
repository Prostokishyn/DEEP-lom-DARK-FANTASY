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
    }

    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI questConditionText;
    public TextMeshProUGUI questRewardText;
    public Button claimButton;

    private List<Quest> quests;
    private int currentQuestIndex;

    public GameManager gameManager;
    public ExpSystem expSystem;

    private void Start()
    {
        quests = new List<Quest>
        {
            new Quest { name = "Money", condition = "Collect 10 coins", rewardGold = 100, rewardExp = 250 },
            new Quest { name = "My house", condition = "Buy a first building", rewardGold = 200, rewardExp = 500 }
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
            if (currentQuestIndex == 0 && gameManager.coin >= 10)
            {
                // ���� ����� ������� ������ ��������, ������� ������ �������������
                claimButton.interactable = true;
            }
            else if (currentQuestIndex == 1 && gameManager.buildingPlaced != null)
            {
                // ���� ����� ������� ������ ��������, ������� ������ �������������
                claimButton.interactable = true;
            }
            else
            {
                // ������ ����������� ������
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
        claimButton.interactable = false; // ����������� ������ �� ��������� �����
    }

    public void ClaimReward()
    {
        if (currentQuestIndex < quests.Count && !quests[currentQuestIndex].completed)
        {
            // ���� ������ ��������� �� ����� �� ��������, �������� ��������
            gameManager.coin += quests[currentQuestIndex].rewardGold;
            expSystem.totalExperience += quests[currentQuestIndex].rewardExp;
            quests[currentQuestIndex].completed = true; // ��������� ����� �� ��� ��������

            currentQuestIndex++;

            if (currentQuestIndex < quests.Count)
            {
                UpdateUI();
            }
            else
            {
                // ���� ����� ���� ������
                questNameText.text = "���� ������";
                questConditionText.text = "�� �������� �� ������!";
                questRewardText.text = "";
                claimButton.gameObject.SetActive(false);
            }
        }
    }
}

