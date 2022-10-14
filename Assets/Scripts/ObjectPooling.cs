using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    [SerializeField] 
    private GameObject _cubePrefab;
    private List<GameObject> _poolObjects = new List<GameObject>();
    private int _defaultAmountObjects = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        for(int i = 0; i < _defaultAmountObjects; i++)
        {
            GameObject obj = Instantiate(_cubePrefab);
            obj.SetActive(false);
            _poolObjects.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            if (!_poolObjects[i].activeInHierarchy)
            {
                return _poolObjects[i];
            }
        }
        GameObject obj = Instantiate(_cubePrefab);
        _poolObjects.Add(obj);
        return obj;
    }
}
