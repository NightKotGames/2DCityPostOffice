using UnityEngine;

[CreateAssetMenu(menuName = "RandomMoveState", fileName = "RandomMoveState")]
public class RandomMoveState : PostManState
{

    [Header("State Options")]
    [SerializeField] private float _maxDistance = 5f;

    [Space(15), SerializeField] private PostManState _nextState;

    private Vector3 _randomPos;

    public override void Init()
    {

        var rand = UnityEngine.Random.Range(-_maxDistance, _maxDistance);
        _randomPos = new Vector3(CurrentPostman.transform.position.x + rand, CurrentPostman.transform.position.y, CurrentPostman.transform.position.z);
        var distance = (_randomPos - CurrentPostman.transform.position).magnitude;
        if (distance > .5f)
        {
            CurrentPostman.MoveToDestinationPoint(_randomPos);
        }
    }

    public override void RandomAction()
    {
    }

    public override void FinalAction()
    {
        CurrentPostman.SetState(_nextState);
    }

}
