using UnityEngine;


public abstract class PostManState : ScriptableObject
{

    [HideInInspector] public PostMan CurrentPostman;
    [HideInInspector] public MonoBehaviour MonoBehaviour;

    public abstract void Init();

    public abstract void RandomAction();

    public abstract void FinalAction();


}
