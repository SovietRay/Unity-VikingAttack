using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void ApplyDamage(GameObject other)
    {
        if (other.TryGetComponent<Health>(out var health))
            health.TakeDamage(_damage);
    }
}
