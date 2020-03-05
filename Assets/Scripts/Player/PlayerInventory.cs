using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int _coinCount = 0;
    public void AddCoin(int quantity)
    {
        _coinCount += quantity;
        Debug.Log($"Toss a coin to your witcher - {_coinCount}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(collision.gameObject))
        {
            AddCoin(GameManager.Instance.coinContainer[collision.gameObject].ReturnCostAndDestroy());
        }
    }
}
