using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area
{ 
    BoatBeach = 0, 
    BoatBeachToTown = 1,
    Town = 2,
    TownToDonutShop = 3,
    DonutShop = 4
}

public class AreaData : MonoBehaviour
{
    [SerializeField] Area IslandArea;

    public static System.Action<Area> AreaEntered;

    private void OnTriggerEnter(Collider other)
    {
        AreaEntered?.Invoke(IslandArea);
    }
}
