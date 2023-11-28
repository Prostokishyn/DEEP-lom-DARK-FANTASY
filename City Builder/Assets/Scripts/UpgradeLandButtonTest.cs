using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using static UnityEngine.EventSystems.EventTrigger;

public class UpgradeLandButtonTest : MonoBehaviour
{
    public GameObject land2;
    public GameObject land1;

    public GameObject upgradeButton;

    public void OnMouseDown()
    {
        land1.SetActive(false);

        land2.SetActive(true);

        Destroy(upgradeButton);

    }
}
