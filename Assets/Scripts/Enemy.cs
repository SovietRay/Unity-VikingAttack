using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private int _damageFromParticle;

    private void Update()
    {
        if (_target == null)
            return;
        
       // if (Vector3.Distance(transform.position, _target.transform.position);
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
