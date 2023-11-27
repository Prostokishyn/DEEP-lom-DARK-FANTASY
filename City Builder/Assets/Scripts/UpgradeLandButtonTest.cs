using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using static UnityEngine.EventSystems.EventTrigger;

public class UpgradeLandButtonTest : MonoBehaviour
{
    public GameObject grid2;
    public Tile[] tiles2;

    public GameObject land2;

    public GameObject land1;

    private Building buildingToPlace;
    public GameObject grid;

    public void OnMouseDown()
    {
        land1.SetActive(!land1.activeSelf);

        land2.SetActive(!land2.activeSelf);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
        {

            Tile nearestTile = null;
            float nearestDistance = float.MaxValue;
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

                //customCursor.gameObject.SetActive(false);
                Cursor.visible = true;
            }
        }
    }
}
