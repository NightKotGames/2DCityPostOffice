using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageAggregator : MonoBehaviour
{

    [SerializeField, Range(1, 15)] private int _packageAmount;
    [SerializeField] private Package _package;
    [SerializeField] private PostManSpawner _postManSpawner;
    [SerializeField] private Transform _postAmptEnterPoint;

    [SerializeField] private List<Package> _parcelWarehouse;

    public static event Action<Package, int?> AddPackageToPostman = delegate { };
    private Queue<Package> _queuePackage;
    


    private void Start()
    {

        _parcelWarehouse = new List<Package>();
        _queuePackage = new Queue<Package>();

        if (_package != null)
        {
            for (int i = 0; i < _packageAmount; i++)
            {
                GameObject package = Instantiate(_package.gameObject, _postAmptEnterPoint);
                _parcelWarehouse.Add(package.gameObject.GetComponent<Package>());
                package.SetActive(false);
            }
        }

    }

    private void OnEnable()
    {
        HomeControl.MakeOrder += AddPackageToQueue;
        HomeControl.CancelOrder += DeletePackageOfQueue;
        DwellingHouse.PackageTaked += ReturnPackageToPool;
    }


    private void OnDisable()
    {
        HomeControl.MakeOrder -= AddPackageToQueue;
        HomeControl.CancelOrder -= DeletePackageOfQueue;
        DwellingHouse.PackageTaked -= ReturnPackageToPool;
    }

    private void AddPackageToQueue(string adress, Transform enterHomePoint)
    {

        Package package = _parcelWarehouse.Find(pack => pack.gameObject.activeSelf == false);

        if (package != null)
        {
            package.SetDestination(adress, enterHomePoint);
            package.gameObject.SetActive(true);
            StartCoroutine(Timer());

            IEnumerator Timer()
            {
                yield return new WaitForSeconds(2f);
                _queuePackage.Enqueue(package);
                int? currentPostmanNumber = _postManSpawner.FindFreePostMan();

                if (currentPostmanNumber != null)
                {
                    AddPackageToPostman.Invoke(package, currentPostmanNumber);
                    currentPostmanNumber = null;
                }
                currentPostmanNumber = null;
            }

            StopCoroutine(Timer());
        }
        else
        {
            throw new System.ArgumentException("Free package box not found:");
        }


    }

    private void DeletePackageOfQueue(string adress, Transform enterHomePoint)
    {
        Package package = _parcelWarehouse.Find(pack => pack.DestinationAdress == adress);
        ReturnPackageToPool(package);
    }

    private void ReturnPackageToPool(Package package)
    {
        package.gameObject.SetActive(false);
        package.ClearDestination();
        package.transform.position = Vector3.zero;
    }


}
