using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HomeControl : MonoBehaviour
{
    [Tooltip("HouseGameObjectComponent")]
    [SerializeField] private DwellingHouse _mainHouse;

    [SerializeField] private Button _makeOrder;
    [SerializeField] private Button _cancelOrder;

    [SerializeField] private float _buttonNonActiveAfterPressed;
    public static event Action<string, Transform> MakeOrder = delegate { };
    public static event Action<string, Transform> CancelOrder = delegate { };

    private void Start()
    {
        _makeOrder.onClick.AddListener(() =>
        {
            _makeOrder.interactable = false;
            MakeOrder.Invoke(_mainHouse.HouseAddress, _mainHouse.MyHomeEnterTransform);
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return new WaitForSeconds(_buttonNonActiveAfterPressed);
                _makeOrder.interactable = true;
                StopCoroutine(Timer());
            }
        });

        _cancelOrder.onClick.AddListener(() =>
        {
            _cancelOrder.interactable = false;
            CancelOrder.Invoke(_mainHouse.HouseAddress, _mainHouse.MyHomeEnterTransform);
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                yield return new WaitForSeconds(_buttonNonActiveAfterPressed);
                _cancelOrder.interactable = true;
                StopCoroutine(Timer());
            }
        });

    }

}
