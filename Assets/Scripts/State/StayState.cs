using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StayState", fileName = "StayState")]
public class StayState : PostManState
{

    [SerializeField] private float _waitToRandomAction;
    [SerializeField] private List<PostManState> _randomState;

    public override void Init()
    {
        CurrentPostman.TimeForRandomState(CurrentPostman.TimeToRandomState);

    }

    public override void RandomAction()
    {
        if (_randomState.Count <= 0) { return; }

        CurrentPostman.SetState(_randomState[UnityEngine.Random.Range(0, _randomState.Count)]);

    }

    public override void FinalAction()
    {

    }


}