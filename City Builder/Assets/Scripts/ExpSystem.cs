using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExpSystem : MonoBehaviour
{
    public GameObject UpgradeLandButton;

    //public Building[] Buildngs;

    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;

    public int currentLevel, totalExperience;
    int previousLevelsExperience, nextLevelsExperience;

    int maxLevel = 5;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    void Start()
    {
        UpdateLevel();
    }

    public GameObject level1Reward;
    public GameObject level2Reward;
    public GameObject level3Reward;
    public GameObject level3_2Reward;
    public GameObject level4Reward;
    public GameObject level4_2Reward;
    public GameObject level5Reward;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //   if (currentLevel < maxLevel)
        //    {
        //        AddExperience(50);
        //    }
        //}

        if (currentLevel >= 1)
        {
            level1Reward.GetComponent<Button>().interactable = true;
        }

        if (currentLevel >= 2)
        {
            level2Reward.GetComponent<Button>().interactable = true;
        }

        if (currentLevel >= 3)
        {
            level3Reward.GetComponent<Button>().interactable = true;
            level3_2Reward.GetComponent<Button>().interactable = true;
        }

        if (currentLevel >= 4)
        {
            level4Reward.GetComponent<Button>().interactable = true;
            level4_2Reward.GetComponent<Button>().interactable = true;
        }

        if (currentLevel == 5)
        {
            level5Reward.GetComponent<Button>().interactable = true;
        }
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    public void CheckForLevelUp()
    {
        if (totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            UpdateLevel();

            // Start level up sequence... Possibly vfx?
        }
    }

    void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    public void UpdateInterface()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience;

        if (currentLevel < maxLevel)
        {
            levelText.text = currentLevel.ToString();
            experienceText.text = start + " exp / " + end + " exp";
            experienceFill.fillAmount = (float)start / (float)end;
        }
        else
        {
            levelText.text = currentLevel.ToString();
            experienceFill.fillAmount =1f;
            experienceText.text = "max";
            UpgradeLandButton.SetActive(true);
        }
    }
}
