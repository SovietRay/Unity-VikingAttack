using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _target;

    private Wave _currentWave;
    private int _waveCount;
    private float _lastSpawnTime;
    private int _spawned;
    
    private void OnEnable()
    {
        if (_spawnPoint == null)
        {
            enabled = false;
            throw new InvalidOperationException(); 
        }

        if (_target == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    void Start()
    {
        SetWave(_waveCount);
    }

    void Update()
    {
        if (_currentWave == null)
            return;
        _lastSpawnTime += Time.deltaTime;

        if (_lastSpawnTime >= _currentWave.Delay)
        {
            var enemy = Instantiate(_currentWave.Prefab, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint);
            enemy.GetComponent<Enemy>().SetTarget(_target);
            _spawned++;
            _lastSpawnTime = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _waveCount + 1)
            {
                SetWave(++_waveCount);
                _spawned = 0;
            }
            else
                _currentWave = null;
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Prefab;
    public float Delay;
    public int Count;
}


