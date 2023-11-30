using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject UserInterface;
    public GameObject PauseInterface;

    public void Pause()
    {
        UserInterface.SetActive(!UserInterface.activeSelf);
        PauseInterface.SetActive(!PauseInterface.activeSelf);
    }

    public void BackToTheGame()
    {
        UserInterface.SetActive(!UserInterface.activeSelf);
        PauseInterface.SetActive(!PauseInterface.activeSelf);
    }

    public void ExitOnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
