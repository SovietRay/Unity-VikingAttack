using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMeleeAttack))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    //public UnityEvent HitEvent;

    [SerializeField] private GameObject _target; //The purpose of the movement can be not only player
    [SerializeField] private int _damageFromParticle;
    [SerializeField] private float _speed;
    [SerializeField] private float _errorСonvergenceCoefficient = 0.5f;

    private Rigidbody2D _rigidbody;
    private EnemyMeleeAttack _enemyMeleeAttack;
    private Health _health;
    private Vector3 _direction;

    private void OnValidate()
    {
        if (_speed < 0)
            _speed = 0;
    }

    private void Start()
    {
        TryGetComponent(out _enemyMeleeAttack);
        TryGetComponent(out _rigidbody);
        TryGetComponent(out _health);
        
        if (_target == null)
        {
            enabled = false;
            throw new InvalidOperationException();
        }
    }

    private void Update()
    {
        if (_target == null)
            return;

        _direction = Vector3.zero;
        MoveToTarget();
    }

    private void OnParticleCollision(GameObject other)
    {
        _health.TakeDamage(_damageFromParticle);
    }

    public void SetTarget (GameObject target)
    {
        if (target != null)
        _target = target;
    }

    public void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) > (_enemyMeleeAttack.AttackRange + _errorСonvergenceCoefficient))
        {
            _direction = Vector3.right;
            _direction *= _speed;
            _direction.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _direction;
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
