using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public int coin;
    public TextMeshProUGUI coinText;

    public int energy;
    public TextMeshProUGUI energyText;

    public float energyReplenishInterval = 5.0f;
    public int energyReplenishAmount = 5;

    public float earningBuildingsInterval = 60.0f;
    public int earningBuildings = 100;

    private Building buildingToPlace;
    public GameObject grid;
    public GameObject grid2;

    public GameObject land1;

    public CustomCursor customCursor;

    public Tile[] tiles;
    public Tile[] tiles2;

    public GameObject messageResources;

    public AudioSource buyBuilding;
    public AudioSource placement;
    public AudioSource message;
    public AudioSource energyRecovery;
    public AudioSource earning;

    public TextMeshProUGUI currentLevel;
    public int lvl;

    private void Start()
    {
        StartCoroutine(ReplenishEnergyRoutine());

        StartCoroutine(GenerateBuildingIncome());
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
            energyRecovery.Play();
        }
    }

    private IEnumerator GenerateBuildingIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(earningBuildingsInterval);
            GenerateIncomeFromBuildings();
        }
    }

    private void GenerateIncomeFromBuildings()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.isOccupied)
            {
                coin += earningBuildings;
                earning.Play();
            }
        }
    }

    private void Update()
    {
        coinText.text = coin.ToString();
        energyText.text = energy.ToString();

        bool wasLand1Active = land1.activeSelf;
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            placement.Play();
            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;

            if (wasLand1Active)
            {
                foreach (Tile tile in tiles)
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
                    grid2.SetActive(false);

                    customCursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                }
            }
            else
            {
                foreach (Tile tile in tiles2)
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
                    grid2.SetActive(false);

                    customCursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                }
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
        currentLevel.text = lvl.ToString();
        bool isLand1Active = land1.activeSelf;
        bool canAfford = coin >= building.cost && energy >= building.energyCost;
        if (canAfford)
        {
            buyBuilding.Play();

            customCursor.gameObject.SetActive(true);
            customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;

            coin -= building.cost;
            energy -= building.energyCost;
            buildingToPlace = building;

            if (isLand1Active)
                grid.SetActive(true);
            else
                grid2.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowMessage(1f));
            message.Play();
        }
    }
}
