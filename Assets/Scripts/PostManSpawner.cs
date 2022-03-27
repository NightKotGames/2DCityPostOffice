using System.Collections.Generic;
using UnityEngine;

public class PostManSpawner : MonoBehaviour
{

    [SerializeField, Range(1, 15)] private int _maxPostManAmount;
    [SerializeField, Range(1f, 10f)] private float _spawnRadius;
    [SerializeField] private Transform _startPointPostMan;
    [SerializeField] private PostMan _postMan;
    private List<PostMan> _postMans;

    private void Start()
    {
        _postMans = new List<PostMan>();

        int currentPostManAmount = UnityEngine.Random.Range(1, _maxPostManAmount);
        int angleStep = 360 / currentPostManAmount;

        for (int i = 0; i < currentPostManAmount; i++)
        {
            if (_postMan == null) { return; }
            GameObject obj = Instantiate(_postMan.gameObject, _startPointPostMan);
            var script = obj.GetComponent<PostMan>();
            script.SetPostManFreeStatus(true);
            _postMans.Add(script);
            obj.transform.position = new Vector3(_spawnRadius * Mathf.Cos(angleStep * (i + 1) * Mathf.Deg2Rad), _startPointPostMan.position.y, _spawnRadius * Mathf.Sin(angleStep * (i + 1) * Mathf.Deg2Rad));
            script.SetNumberPostMan(i);
            script.SetStartPosition(obj.transform);

        }

    }

    public int FindFreePostMan()
    {


        try
        {
            return _postMans.Find(man => man.FreeStatus == true).NumberPostMan;
        }
        catch
        {
            throw new System.ArgumentException("Free PostMan not found:"); //return _postMans[UnityEngine.Random.Range(0, _postMans.Count)];
        }

    }


}
