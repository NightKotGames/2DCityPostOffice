using UnityEngine;

[CreateAssetMenu(menuName = "GivePackageState", fileName = "GivePackageState")]
public class GivePackageState : PostManState
{

    [SerializeField] private PostManState _finalState;

    public override void Init()
    {
        CurrentPostman.SetLostState(this);
        CurrentPostman.DropPackage();
        FinalAction();
    }

    public override void RandomAction()
    {

    }

    public override void FinalAction()
    {
        CurrentPostman.SetState(_finalState);
    }
}
