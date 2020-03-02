using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _cost = 1;

    private bool _used = false;

    private void Start()
    {
        GameManager.Instance.coinContainer.Add(gameObject, this);
    }

    public int ReturnCostAndDestroy()
    {
        if (!_used)
        {
            _animator.SetTrigger("StartDestroy");
            _used = true;
            return _cost;
        }
        else
            return 0;
    }
    private void EndDestroy()
    {
        Destroy(gameObject);
    }
}
