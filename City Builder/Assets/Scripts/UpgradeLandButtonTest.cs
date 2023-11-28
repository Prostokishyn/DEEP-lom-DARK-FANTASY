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
    public int upgradeLandCost;

    //private int coin;
    public TextMeshProUGUI coinText;

    public GameObject land2;
    public GameObject land1;

    public GameObject upgradeButton;

    public GameObject upgradeLandMessage;
    public AudioSource message;

    IEnumerator ShowUpgradeLandMessage(float seconds)
    {
        upgradeLandMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(seconds);

        upgradeLandMessage.gameObject.SetActive(false);
    }

    public void OnMouseDown(GameManager gameManager)
    {
        //coinText.text=coin.ToString();
        //upgradeLandCost.ToString();
        if (gameManager.coin >= upgradeLandCost)
        {
            land1.SetActive(false);

            land2.SetActive(true);
            gameManager.coin -= upgradeLandCost;

            Destroy(upgradeButton);
        }
        else
        {
            StartCoroutine(ShowUpgradeLandMessage(1f));
            message.Play();
        }
    }
}
