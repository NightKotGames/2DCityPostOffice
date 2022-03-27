using TMPro;
using UnityEngine;

public class HomeView : MonoBehaviour
{

    [SerializeField] private DwellingHouse _house;
    [SerializeField] private TextMeshProUGUI _homeAddressText;


    private void Start()
    {
        if (_house != null)
        {
            _homeAddressText.text = _house.HouseAddress;
        }

    }


}
