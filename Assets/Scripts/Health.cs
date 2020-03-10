using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public float CurrentHealth => _currentHealth;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private UnityEvent _deadEvent;

    private void OnValidate()
    {
        if (_maxHealth < _currentHealth)
            _maxHealth = _currentHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);
        if (_currentHealth <= 0)
        {
            _deadEvent.Invoke();
        }
    }

    public void AddHealth(int health)
    {
        if (health >= 0)
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, _maxHealth);
    }
}
