using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;

    public void SpawnOnEnemy()
    {
        if (_coinPrefab != null)
            Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }
}
