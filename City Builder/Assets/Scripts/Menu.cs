using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject LevelsMenu;

    public void OpenCloseLevels()
    {
        menu.SetActive(!menu.activeSelf);
        LevelsMenu.SetActive(!LevelsMenu.activeSelf);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayLvl1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLvl2()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayLvl3()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
