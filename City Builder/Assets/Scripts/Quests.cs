using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public GameObject QuestsMenu;

    public GameObject shopMenuOff;

    public AudioSource openQuests;

    public void OpenCloseQuests()
    {
        QuestsMenu.SetActive(!QuestsMenu.activeSelf);
        openQuests.Play();

        shopMenuOff.SetActive(false);
    }
}
