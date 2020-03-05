using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    [SerializeField] int _hpAmount;
    [SerializeField] private Animator _animator;

    private bool _used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _) && collision.TryGetComponent<Health>(out var healthTemp) && !_used)
        {
            healthTemp.AddHealth(_hpAmount);
            Destroy();
        }
    }

    public void Destroy()
    {
        _animator.SetTrigger("StartDestroy");
        _used = true;
    }

    private void EndDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
