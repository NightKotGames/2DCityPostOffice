using System;
using UnityEngine;

public class DwellingHouse : MonoBehaviour
{


    [SerializeField] private string _houseAddress;
    public string HouseAddress { get { return _houseAddress; } protected set { } }

    [SerializeField] private EnterPoint _enterPoint;

    public Transform MyHomeEnterTransform { get; private set; }
    public static event Action<Package> PackageTaked = delegate { };

    private void Awake()
    {
        MyHomeEnterTransform = _enterPoint.transform;
    }

    public void TakePackage(Package pack)
    {
        PackageTaked.Invoke(pack);
    }


}
