using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMenu : MonoBehaviour
{
    public GameManager gameManager;

    public ExpSystem expSystem;

    public GameObject cheatmenu;

    public void OnMouseDown()
    {
        cheatmenu.SetActive(!cheatmenu.activeSelf);
    }

    public void addCoin()
    {
        gameManager.coin += 100;
    }

    public void minusCoin()
    {
        gameManager.coin -= 100;
    }

    public void maxLvl()
    {
        expSystem.currentLevel = 5;

        expSystem.CheckForLevelUp();
        expSystem.UpdateInterface();
    }
}
