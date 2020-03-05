using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void SetDamage(GameObject other)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(other))
            GameManager.Instance.healthContainer[other].TakeDamage(_damage);
    }
}
