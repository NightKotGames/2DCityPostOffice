using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CarryPackage", fileName = "CarryPackage")]
public class CarryPackageState : PostManState
{

    [SerializeField] private PostManState _finalState;
    [Space(15), SerializeField] private List<PostManState> _randomState;

    public override void Init()
    {
        CurrentPostman.SetLostState(this);
        CurrentPostman.MoveToDestinationPoint(CurrentPostman.CurrentPackage.DestinationTransform.position);

    }

    public override void RandomAction()
    {

    }

    public override void FinalAction()
    {
        CurrentPostman.SetState(_finalState);
    }
}
