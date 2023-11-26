using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject shopMenu;

    public void OnMouseDown()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
}
