using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinText;

    private Building buildingToPlace;
    public GameObject grid;

    public CustomCursor customCursor;

    private void Update()
    {
        coinText.text = coin.ToString();
    }

    public void BuyBuilding(Building building)
    {
        if (coin >= building.cost)
        {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;

            coin -= building.cost;
            buildingToPlace = building;
            grid.SetActive(true);
        }
    }
}
