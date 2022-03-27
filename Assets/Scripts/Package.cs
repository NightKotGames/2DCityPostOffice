using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Package : MonoBehaviour
{
    [SerializeField] private string _destinationAdress;
    public string DestinationAdress { get { return _destinationAdress; } }

    [SerializeField] private int _usesPostmanNumber;
    public int UsesPostmanNumber { get { return _usesPostmanNumber; } }

    public Transform DestinationTransform { get; private set; }

    private BoxCollider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetPhysicsEnabled(bool enable)
    {
        _collider.enabled = enable;
        _rigidbody.useGravity = enable;
    }

    public void SetDestination(string destinationAdress, Transform destinationTransform)
    {
        _destinationAdress = destinationAdress;
        DestinationTransform = destinationTransform;
    }

    public void ClearDestination()
    {
        _destinationAdress = "";
        DestinationTransform = null;
    }


}
