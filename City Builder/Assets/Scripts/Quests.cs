using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public GameObject QuestsMenu;

    public GameObject shopMenuOff;

    public void OpenCloseQuests()
    {
        QuestsMenu.SetActive(!QuestsMenu.activeSelf);

        shopMenuOff.SetActive(false);
    }
}
