using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _reloadTime = 0.7f;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private float _attackRange;

    private Animator _animator;
    private bool _canHit = true;

    public float AttackRange => _attackRange;

    private void OnEnable()
    {
        if (_attackPoint == null)
        {
            enabled = false;
            throw new InvalidOperationException(_attackPoint.name);
        }
    }

    private void Start()
    {
        TryGetComponent(out _animator);
    }

    void Update()
    {
        if (_canHit)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        Collider2D[] hitObject = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _attackMask);
        foreach (Collider2D obj in hitObject)
        {
            if (obj.gameObject.TryGetComponent<Health>(out var health))
            {
                _animator.SetTrigger("Attack");
                health.TakeDamage(_damage);
                _canHit = false;
                yield return new WaitForSeconds(_reloadTime);
                _canHit = true;
            }
        }
        yield return null;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
