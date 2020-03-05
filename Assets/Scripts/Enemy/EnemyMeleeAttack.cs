using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _reloadTime = 0.7f;
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _attackMask;
    [SerializeField] private float _attackRange;
    public float AttackRange => _attackRange;

    private bool _canHit = true;

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
            if (GameManager.Instance.healthContainer.ContainsKey(obj.gameObject))
            {
                _animator.SetTrigger("Attack");
                GameManager.Instance.healthContainer[obj.gameObject].TakeDamage(_damage);
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
