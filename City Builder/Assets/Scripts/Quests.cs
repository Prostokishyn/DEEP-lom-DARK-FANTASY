using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public GameObject QuestsMenu;

    public GameObject shopMenuOff;

    public AudioSource openQuests;

    [SerializeField] Animator quests;
    private bool isQuestsOpen = false;

    public void OpenCloseQuests()
    {
        QuestsMenu.SetActive(true);
        openQuests.Play();
        isQuestsOpen = !isQuestsOpen;
        quests.SetBool("QuestsMenu", isQuestsOpen);

        //shopMenuOff.SetActive(false);
    }
}
