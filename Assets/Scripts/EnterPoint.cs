using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnterPoint : MonoBehaviour
{
    public Transform EnterPointTransform { get; private set; }
    public bool ActivateEnterPoint { get; private set; }

    private void Awake()
    {
        EnterPointTransform = gameObject.transform;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Package pack))
        {
            ActivateEnterPoint = true;
        }
        else
        {
            ActivateEnterPoint = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Package pack))
        {
            ActivateEnterPoint = false;
        }
    }



}