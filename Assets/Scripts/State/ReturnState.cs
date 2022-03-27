using UnityEngine;

[CreateAssetMenu(menuName = "ReturnState", fileName = "ReturnState")]
public class ReturnState : PostManState
{

    [SerializeField] private PostManState _finalState;


    public override void Init()
    {
        CurrentPostman.SetLostState(this);
        CurrentPostman.PackageDelivered();
    }

    public override void RandomAction()
    {

    }

    public override void FinalAction()
    {
        CurrentPostman.SetLostState(null);
        CurrentPostman.SetState(_finalState);
    }
}
