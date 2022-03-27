using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HouseInterfaseHandler : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private Canvas _mainInterface;
    [SerializeField, Range(1f, 10f)] private float _interfaceActiveTime;

    private void Awake()
    {
        if (_mainInterface != null) { _mainInterface.enabled = false; }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_mainInterface == null) { return; }
        _mainInterface.enabled = true;
        StartActivateInterfaceTime(_interfaceActiveTime);
    }

    private void StartActivateInterfaceTime(float _time)
    {
        StartCoroutine(Timer());
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(_time);
            _mainInterface.enabled = false;
        }
        StopCoroutine(Timer());
    }
}
