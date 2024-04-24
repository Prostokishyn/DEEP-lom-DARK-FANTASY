using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject UserInterface;
    public GameObject resources;
    public GameObject QuestsMenu;
    public GameObject PauseInterface;
    public GameObject Settings;
    public GameObject audioButton;
    public GameObject shopMenu;


    public AudioListener music;
    public AudioSource musicSource;

    public AudioSource openMenu;

    public Slider AudioSlider;

    public Sprite audioOff;
    public Sprite audioOn;

    void Update()
    {
        musicSource.volume = AudioSlider.value;
    }

    public void Pause()
    {

        UserInterface.SetActive(false);
        PauseInterface.SetActive(true);
        QuestsMenu.SetActive(false);
        shopMenu.SetActive(false);
    }

    public void BackToTheGame()
    {
        UserInterface.SetActive(true);
        PauseInterface.SetActive(false);
    }

    public void OpenCloseAudioSettings()
    {
        PauseInterface.SetActive(!PauseInterface.activeSelf);
        Settings.SetActive(!Settings.activeSelf);
    }

    public void AuidoSettings()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            audioButton.GetComponent<Image>().sprite = audioOff;
        }
        else
        {
            AudioListener.volume = 1;
            audioButton.GetComponent<Image>().sprite = audioOn;
        }
    }

    public void ExitOnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
