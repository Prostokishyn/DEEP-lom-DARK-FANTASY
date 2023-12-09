using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ExpSystem : MonoBehaviour
{
    public GameObject UpgradeLandButton;

    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;

    int currentLevel, totalExperience;
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentLevel < maxLevel)
            {
                AddExperience(50);
            }
        }
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
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

    void UpdateInterface()
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
