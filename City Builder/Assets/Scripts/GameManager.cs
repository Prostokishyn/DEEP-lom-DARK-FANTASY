using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinText;

    public int energy;
    public TextMeshProUGUI energyText;

    public float energyReplenishInterval = 5.0f;
    public int energyReplenishAmount = 5;

    private Building buildingToPlace;
    public GameObject grid;

    public CustomCursor customCursor;

    public Tile[] tiles;

    public GameObject messageResources;

    private void Start()
    {
        StartCoroutine(ReplenishEnergyRoutine());
    }

    private IEnumerator ReplenishEnergyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(energyReplenishInterval);
            ReplenishEnergy();
        }
    }

    private void ReplenishEnergy()
    {
        if (energy < 100)
        {
            energy = Mathf.Min(100, energy + energyReplenishAmount);
        }
    }

    private void Update()
    {
        coinText.text = coin.ToString();
        energyText.text = energy.ToString();
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;
            foreach(Tile tile in tiles)
            {
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearestTile = tile;
                }
            }
            if (nearestTile.isOccupied == false)
            {
                Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
                buildingToPlace = null;
                nearestTile.isOccupied = true;
                grid.SetActive(false);

                customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
    }

    IEnumerator ShowMessage(float seconds)
    {
        messageResources.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        messageResources.gameObject.SetActive(false);
    }

    public void BuyBuilding(Building building)
    {
        if (coin >= building.cost && energy>=building.energyCost)
        {
            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;

            coin -= building.cost;
            energy -= building.energyCost;
            buildingToPlace = building;
            grid.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowMessage(1f));
        }
    }
}
