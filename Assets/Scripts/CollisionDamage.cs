using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void SetDamage(GameObject other)
    {
        if (other.TryGetComponent<Health>(out var healthTemp))
            healthTemp.TakeDamage(_damage);
    }
}
