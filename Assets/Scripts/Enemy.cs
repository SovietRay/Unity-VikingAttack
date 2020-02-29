using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 _direction;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Player _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private int _damageFromParticle;
    [SerializeField] private float speed;

    private void Update()
    {
        if (_target == null)
            return;

        _direction = Vector3.zero;

        if (Vector3.Distance(transform.position, _target.transform.position) > 1.4f)
        {
            _direction = Vector3.right;
            _direction *= speed;
            _direction.y = _rigidbody.velocity.y;
            _rigidbody.velocity = _direction;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        _health.TakeDamage(_damageFromParticle);
    }
    public void SetTarget (Player target)
    {
        _target = target;
    }
}
