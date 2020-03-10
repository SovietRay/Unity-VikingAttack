using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int _coinCount = 0;

    public void AddCoin(int quantity)
    {
        _coinCount += quantity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Coin>(out var coin))
            AddCoin(coin.ReturnCostAndDestroy());
    }
}
