using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyMeleeAttack))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Player _target;
    [SerializeField] private Health _health;
    [SerializeField] private int _damageFromParticle;
    [SerializeField] private float _speed;

    private EnemyMeleeAttack _enemyMeleeAttack;
    private Vector3 _direction;
    private float _errorCoefficient = 0.5f;

    private void OnValidate()
    {
        if (_speed < 0)
            _speed = 0;
    }
    private void Start()
    {
        _enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
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
    public void SetTarget (Player target)
    {
        _target = target;
    }
    public void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) > (_enemyMeleeAttack.AttackRange + _errorCoefficient))
        {
            _direction = Vector3.right;
            _direction *= _speed;
            _direction.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _direction;
        }
    }
}
