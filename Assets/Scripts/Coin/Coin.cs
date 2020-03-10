using UnityEngine;

[RequireComponent(typeof (Animator))]
public class Coin : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _cost = 1;

    private void Start()
    {
        TryGetComponent(out _animator);
    }

    public int ReturnCostAndDestroy()
    {
        if (gameObject.TryGetComponent<CircleCollider2D>(out var collider2D))
        {
            _animator.SetTrigger("StartDestroy");
            collider2D.enabled = false;
            return _cost;
        }
        else
            return 0;
    }
    private void EndDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
