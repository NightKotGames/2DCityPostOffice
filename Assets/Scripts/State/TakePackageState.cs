using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TakePackageState", fileName = "TakePackageState")]
public class TakePackageState : PostManState
{

    [SerializeField] private float _waitToRandomAction;
    [SerializeField] private List<PostManState> _randomState;
    [Space(15), SerializeField] private PostManState _finalState;

    public override void Init()
    {
        CurrentPostman.SetLostState(this);
        CurrentPostman.MoveToDestinationPoint(CurrentPostman.CurrentPackage.transform.position);
        RandomAction();
    }

    public override void RandomAction()
    {
        if (_randomState.Count <= 0) { return; }

    }

    public override void FinalAction()
    {
        CurrentPostman.KeepCurrentPackage();
        CurrentPostman.SetState(_finalState);
    }

}