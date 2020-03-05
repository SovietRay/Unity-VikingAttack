using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private GameObject _coinPrefab;

    private void OnValidate()
    {
        if (_maxHealth < _currentHealth)
            _maxHealth = _currentHealth;
    }
    private void Start()
    {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }
    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        //Debug.Log($"My name - {gameObject.name}, i take {damage} damage! My current health {_currentHealth} from {_health}.");
        if (_currentHealth <= 0)
        {
            if (_coinPrefab != null)
                Instantiate(_coinPrefab, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void AddHealth(int health)
    {
        if (health >= 0)
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, _maxHealth);
        //Debug.Log($"My name - {gameObject.name}, i take {health} health! My current health {_currentHealth} from {_health}.");
    }
}
