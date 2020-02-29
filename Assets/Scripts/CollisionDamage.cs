using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    private void OnParticleCollision(GameObject other)
    {
        //SetDamage(other.);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public void SetDamage(GameObject other)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(other))
            GameManager.Instance.healthContainer[other].TakeDamage(_damage);
    }
    
}
