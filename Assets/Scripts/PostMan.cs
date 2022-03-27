using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMan : MonoBehaviour
{

    #region Fields

    [Header("Postman Options: ")]

    [SerializeField] private int _numberPostMan;
    public int NumberPostMan { get { return _numberPostMan; } }

    [SerializeField] private float _timeToRandomState;
    public float TimeToRandomState { get { return _timeToRandomState; } }

    [Space(15), SerializeField] private float _mainSpeed;
    [SerializeField] private bool _freeStatus;

    [Header("TimeToReturnToLostState: ")]
    [SerializeField] private float _timeReturnLostState;

    public bool FreeStatus { get { return _freeStatus; } }
    [SerializeField] private bool _cargoLoaded;
    public bool CargoLoaded { get { return _cargoLoaded; } }

    [SerializeField] private Transform _baggageCompartment;
    public Transform BaggageCompartment { get { return _baggageCompartment; } }

    private Vector3 _startPosition;
    public Vector3 StartPosition { get { return _startPosition; } }

    [SerializeField] private Package _currentPackage;
    public Package CurrentPackage { get { return _currentPackage; } }

    private Queue<Package> _packageQueue;



    #region STATE

    [Header("PostMan State Kit: ")]
    [Space(15), SerializeField] private PostManState _startState;
    [SerializeField] private PostManState _takePackageState;
    [SerializeField] private PostManState _returnState;
    [Space(5), SerializeField] private PostManState _currentState;
    [Space(5), SerializeField] private PostManState _lostState;


    #endregion

    #endregion

    #region Public Set Fields Method

    public void SetNumberPostMan(int numberPostMan)
    {
        _numberPostMan = numberPostMan;
    }
    public void SetPostManFreeStatus(bool status)
    {
        _freeStatus = status;
    }
    public void SetStartPosition(Transform startPosition)
    {
        _startPosition = new Vector3(startPosition.position.x, startPosition.position.y, startPosition.position.z);
    }
    public void SetCarryngPackage(bool cargoLoaded)
    {
        _cargoLoaded = cargoLoaded;
        _currentPackage.SetPhysicsEnabled(!cargoLoaded);
    }
    public void SetCurrentPackage(Package currentPackage)
    {
        _currentPackage = currentPackage;
    }
    public void KeepCurrentPackage()
    {
        _cargoLoaded = true;
        _currentPackage.transform.position = _baggageCompartment.position;
    }

    #endregion

    #region Start

    private void Start()
    {
        _packageQueue = new Queue<Package>();
        SetState(_startState);
        TimeForRandomState(_timeToRandomState);

    }

    private void OnEnable()
    {
        PackageAggregator.AddPackageToPostman += AddPackageToQueue;
    }

    private void OnDisable()
    {
        PackageAggregator.AddPackageToPostman -= AddPackageToQueue;
    }


    #endregion

    #region SetState

    public void SetState(PostManState state)
    {
        _currentState = Instantiate(state);
        _currentState.CurrentPostman = this;
        _currentState.Init();

    }

    public void SetLostState(PostManState state)
    {
        _lostState = state;

    }

    #endregion

    #region Select State Methods

    #region Package

    public void AddPackageToQueue(Package package, int? postmanNumber)
    {
        if (postmanNumber != NumberPostMan & _freeStatus == false) { return; }
        _packageQueue.Enqueue(package);
        
        if(_currentPackage == null)
        {
            _currentPackage = package;
            SetState(_takePackageState);
        }

    }

    public void PackageDelivered()
    {

        _packageQueue.Dequeue();
        Debug.Log($"Queue Package: {_packageQueue.Count}");

        if (_packageQueue.Count <= 0)
        {
            MoveToDestinationPoint(_startPosition);
            return;

        }

        TakePackage();


    }

    public void TakePackage()
    {
        Package package = _packageQueue.Peek();
        if (_currentPackage == null)
        {
            _currentPackage = package;
        }

        SetState(_takePackageState);
    }

    #endregion

    #endregion

    #region Move

    public void MoveToDestinationPoint(Vector3 target)
    {

        var _distance = (target - transform.position).magnitude;

        StartCoroutine(StartMove());
        IEnumerator StartMove()
        {

            while (_distance > 1f)
            {

                yield return new WaitForEndOfFrame();
                _distance = (target - transform.position).magnitude;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, target.z), _mainSpeed * Time.deltaTime);

                //Carring Package
                if (_cargoLoaded == true)
                {
                    _currentPackage.transform.position = _baggageCompartment.position;
                }

                if (_distance < 1f)
                {

                    StopCoroutine(StartMove());

                    if(_currentState == _returnState & _packageQueue.Count > 0)
                    {
                        _currentPackage = _packageQueue.Peek();
                        SetState(_takePackageState);
                    }

                    _currentState.FinalAction();
                }

            }

        }

        StopCoroutine(StartMove());

    }


    #endregion

    #region Other Public Methods

    #region Coroutine

    public void TimeForRandomState(float time)
    {
        if (time <= 0) { return; }

        StartCoroutine(TimeForRandomAction());

        IEnumerator TimeForRandomAction()
        {
            yield return new WaitForSeconds(time);

            if (_currentState != null)
            {
                _currentState.RandomAction();
                ReturnToWork(_timeReturnLostState);

            }

            StopCoroutine(TimeForRandomAction());

        }


    }


    public void ReturnToWork(float time)
    {

        if (_lostState == null)
        {
            TimeForRandomState(_timeToRandomState);
            return;
        }

        StartCoroutine(ReturnToLastState());

        IEnumerator ReturnToLastState()
        {
            yield return new WaitForSeconds(time);
            TimeForRandomState(_timeToRandomState);
            if (_lostState != null)
            {
                SetState(_lostState);
            }

            _lostState = null;

        }

        StopCoroutine(ReturnToLastState());

    }

    #endregion

    public void DropPackage()
    {
        _cargoLoaded = false;
        _freeStatus = true;
        SetCarryngPackage(_cargoLoaded);
        _currentPackage = null;
    }


    #endregion



}
