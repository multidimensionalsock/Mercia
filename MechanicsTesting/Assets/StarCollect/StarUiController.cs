using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarUiController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI starCounter;
    int totalStars;
    // Start is called before the first frame update
    void Start()
    {
        StarPickupLogic.starCollected += AddStar;
    }

    void AddStar()
    {
        totalStars++;
        starCounter.text = totalStars.ToString();
    }
}
