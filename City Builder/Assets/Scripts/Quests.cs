using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public GameObject QuestsMenu;

    public GameObject energyOff;
    public GameObject shopOff;

    public void OpenCloseQuests()
    {
        QuestsMenu.SetActive(!QuestsMenu.activeSelf);

        energyOff.SetActive(!energyOff.activeSelf);
        shopOff.SetActive(!shopOff.activeSelf);
    }
}
