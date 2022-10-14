using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private InputField _inputSpeed;
    [SerializeField] private InputField _inputDistance;
    [SerializeField] private InputField _inputTime;
    
    public float _nextSpawnTime = 0;
    private List<GameObject> _cubes = new List<GameObject>();
    
    private void Update()
    {
        if (Time.time >= GetInputValue(_inputTime) + _nextSpawnTime && GetInputValue(_inputTime) != 0)
        {
            _nextSpawnTime = Time.time;
            CubeInstantiate();
        }
        CubesMovement();    
    }

    private void CubeInstantiate()
    {
        GameObject cube = ObjectPooling.Instance.GetObject();
        _cubes.Add(cube);
        if (cube != null)
        {
            cube.transform.position = _spawnPoint.position;
            cube.SetActive(true);
        }
    }

    private void CubesMovement()
    {
        for (int i = 0; i < _cubes.Count; i++)
        {
            _cubes[i].transform.Translate(Vector3.forward * GetInputValue(_inputSpeed) * Time.deltaTime);
            if (Vector3.Distance(_cubes[i].transform.position, _spawnPoint.position) >= GetInputValue(_inputDistance))
            {
                _cubes[i].SetActive(false);
                _cubes.RemoveAt(i);
            }
        }
    }

    private int GetInputValue(InputField input)
    {
        if(input.text.Length > 0)
        {
            return int.Parse(input.text);
        }
        return 1;
    }
}

