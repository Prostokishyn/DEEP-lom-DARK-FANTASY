using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // ресурси
    public int coin;
    public TextMeshProUGUI coinText;

    public int energy;
    public TextMeshProUGUI energyText;

    public float energyReplenishInterval = 5.0f;
    public int energyReplenishAmount = 5;

    public float earningBuildingsInterval = 60.0f;
    public int earningBuildings = 100;

    // придбання та побудова
    private Building buildingToPlace;
    public Building buildingPlaced;

    public GameObject grid;
    public GameObject grid2;

    public Tile[] tiles;
    public Tile[] tiles2;

    private List<Building> purchasedBuildings = new List<Building>();

    public GameObject land1;

    // повідомлення про недостатню кількість ресурсів
    public GameObject messageResources;

    public GameObject shop;

    // звукове супроводження
    public AudioSource buyBuilding;
    public AudioSource placement;
    public AudioSource message;
    public AudioSource energyRecovery;
    public AudioSource earning;

    // рівень
    public TextMeshProUGUI currentLevel;
    public int lvl;

    public GameObject deleteButton;
    public GameObject moveButton;

    private Building buildingToMove;
    private bool isMovingBuilding = false;

    private void Start()
    {
        StartCoroutine(ReplenishEnergyRoutine());
        StartCoroutine(GenerateBuildingIncome());

        // Встановити кнопки неактивними за замовчуванням
        deleteButton.SetActive(false);
        moveButton.SetActive(false);
    }

    private void Update()
    {
        coinText.text = coin.ToString();
        energyText.text = energy.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            ToggleMenu();
        }

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {
            PlaceBuilding();
        }
        else if (Input.GetMouseButtonDown(0) && isMovingBuilding)
        {
            MoveBuildingToNewTile();
        }

        //if (Input.touchCount > 0)
        //{
          //  isTouchingScreen = true;
        //}
        //else
        //{
         //   isTouchingScreen = false;
        //}
    }

    [SerializeField] Animator anim;
    [SerializeField] Animator energy_recover;
    [SerializeField] Animator gold_earn;

    public GameObject menuUi;
    private bool isMenuOpen = false;
    public GameObject ClickHereText;
    //private bool isTouchingScreen = false; // Прапорець, що показує, чи є дотик на екрані

    public void ToggleMenu()
    {
        //Перевірка, чи не відбувається дотик у певній області екрану, наприклад, у верхній частині (залежить від вашого інтерфейсу)
        if (Input.mousePosition.y < Screen.height * 0.3f)
        {
            Destroy(ClickHereText);
            menuUi.SetActive(true);
            isMenuOpen = !isMenuOpen;
            anim.SetBool("Buttons animation", isMenuOpen);
        }
    }
    public void ShowDeleteButton(Building building)
    {
        deleteButton.SetActive(!deleteButton.activeSelf); // Показуємо кнопку видалення
        deleteButton.GetComponent<Button>().onClick.RemoveAllListeners();
        deleteButton.GetComponent<Button>().onClick.AddListener(() => RemoveBuilding(building));
    }

    public void ShowMoveButton(Building building)
    {
        moveButton.SetActive(!moveButton.activeSelf); // Показуємо кнопку переміщення
        moveButton.GetComponent<Button>().onClick.RemoveAllListeners();
        moveButton.GetComponent<Button>().onClick.AddListener(() => EnableMoveBuilding(building));
    }

    private void RemoveBuilding(Building building)
    {
        purchasedBuildings.Remove(building);

        Tile occupiedTile = null;

        foreach (Tile tile in tiles)
        {
            if (tile.transform.position == building.transform.position)
            {
                occupiedTile = tile;
                break;
            }
        }

        if (occupiedTile == null)
        {
            foreach (Tile tile in tiles2)
            {
                if (tile.transform.position == building.transform.position)
                {
                    occupiedTile = tile;
                    break;
                }
            }
        }

        if (occupiedTile != null)
        {
            occupiedTile.isOccupied = false;
        }

        Destroy(building.gameObject);
        deleteButton.SetActive(false); // Приховуємо кнопку видалення після видалення будівлі
        moveButton.SetActive(false); // Приховуємо кнопку переміщення після видалення будівлі
    }

    private void EnableMoveBuilding(Building building)
    {
        buildingToMove = building;
        isMovingBuilding = true;
        deleteButton.SetActive(false); // Приховуємо кнопку видалення
        moveButton.SetActive(false); // Приховуємо кнопку переміщення

        // Активуємо сітку для вибору нової плитки
        grid.SetActive(true);
        grid2.SetActive(true);
    }

    private Tile previousTile;

    private void PlaceBuilding()
    {
        Tile nearestTile = GetNearestTile();

        if (nearestTile != null && !nearestTile.isOccupied && buildingToPlace != null)
        {
            Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
            buildingToPlace = null;
            nearestTile.isOccupied = true;

            // Free up the previously occupied tile if there was one
            if (previousTile != null && previousTile != nearestTile)
            {
                previousTile.isOccupied = false;
            }

            // Set the current tile as the previous tile
            previousTile = nearestTile;

            grid.SetActive(false);
            grid2.SetActive(false);
        }

        if (nearestTile == null && previousTile != null)
        {
            previousTile.isOccupied = false;
        }
    }

    private void MoveBuildingToNewTile()
    {
        Tile nearestTile = GetNearestTile();

        if (nearestTile != null && !nearestTile.isOccupied && buildingToMove != null)
        {
            // Free up the previously occupied tile if there was one
            if (previousTile != null && previousTile != nearestTile)
            {
                previousTile.isOccupied = false;
            }

            // Move the building to the new tile
            buildingToMove.transform.position = nearestTile.transform.position;
            nearestTile.isOccupied = true;
            previousTile = nearestTile;

            grid.SetActive(false);
            grid2.SetActive(false);
            isMovingBuilding = false;
        }
    }

    private Tile GetNearestTile()
    {
        Tile nearestTile = null;
        float nearestDistance = float.MaxValue;
        bool wasLand1Active = land1.activeSelf;

        placement.Play();

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
        }

        return nearestTile;
    }

    // відновлення енергії та дохід з будинків
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
            energy_recover.SetTrigger("EnergyRecover");
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
        foreach (Building building in purchasedBuildings)
        {
            coin += building.income;
            earning.Play();
            gold_earn.SetTrigger("Earn");
        }
    }

    // повідомлення про недостатню кількість ресурсів
    IEnumerator ShowMessage(float seconds)
    {
        messageResources.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        messageResources.gameObject.SetActive(false);
    }

    // придбання будівль
    public void BuyBuilding(Building building)
    {
        bool isLand1Active = land1.activeSelf;
        bool canAfford = coin >= building.cost && energy >= building.energyCost;
        if (canAfford)
        {
            purchasedBuildings.Add(building);
            buyBuilding.Play();

            Cursor.visible = false;

            coin -= building.cost;
            energy -= building.energyCost;
            buildingToPlace = building;

            if (isLand1Active)
                grid.SetActive(true);
            else
                grid.SetActive(true);
            grid2.SetActive(true);

            buildingPlaced = buildingToPlace;
            shop.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(ShowMessage(1f));
            message.Play();
        }
    }
}
