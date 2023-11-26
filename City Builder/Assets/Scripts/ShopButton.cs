using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject shopMenu;

    public AudioSource clickSound;

    public void OnMouseDown()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
}
