using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject shopMenu;

    public GameObject questsMenu;

    public AudioSource clickSound;

    public void OnMouseDown()
    {
        clickSound.Play();
        questsMenu.SetActive(false);
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
}
