using UnityEngine;

[RequireComponent(typeof (Animator))]
public class HealPotion : MonoBehaviour
{
    [SerializeField] private int _hpAmount;
    
    private Animator _animator;

    private void Start()
    {
        TryGetComponent(out _animator);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<BoxCollider2D>(out var collider2D);
        if (collision.TryGetComponent(out Player _) && collision.TryGetComponent<Health>(out var healthTemp) && collider2D.enabled)
        {
            healthTemp.AddHealth(_hpAmount);
            Destroy();
        }
    }

    public void Destroy()
    {
        _animator.SetTrigger("StartDestroy");
        if (TryGetComponent<BoxCollider2D>(out var collider2D))
            collider2D.enabled = false;
    }

    private void EndDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
