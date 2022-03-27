using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{

    [SerializeField] private int _minHouseEnableAmount;
    [SerializeField] private int _maxHouseEnableAmount;
    [SerializeField] private List<DwellingHouse> _house;

    private void Start()
    {
        int currentHouseAmount = UnityEngine.Random.Range(_minHouseEnableAmount, _maxHouseEnableAmount);

        foreach (DwellingHouse house in _house)
        {
            house.gameObject.SetActive(false);
        }

        for (int i = 0; i < currentHouseAmount; i++)
        {
            DwellingHouse currentHouse = _house[i];
            currentHouse.gameObject.SetActive(true);
        }
    }



}
