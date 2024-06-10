using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickupLogic : MonoBehaviour
{
    public static System.Action starCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        starCollected?.Invoke();
        Destroy(gameObject);
    }
}
