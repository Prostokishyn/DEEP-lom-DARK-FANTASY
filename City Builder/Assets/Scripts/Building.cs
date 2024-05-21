using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public int cost;
    public int energyCost;
    public int income;
    public bool placed = false;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager не знайдено!");
        }
        Debug.Log("Building initialized: " + this.name);
    }

    private void OnMouseDown()
    {
        gameManager.ShowDeleteButton(this);
        gameManager.ShowMoveButton(this);
    }
}
