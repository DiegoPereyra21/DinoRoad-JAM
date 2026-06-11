using System.Threading;
using UnityEngine;

public class ScoreObstacleScript : MonoBehaviour
{
    public int rewardCoins = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinManager.TotalCoins += rewardCoins;
            Debug.Log($"Added: {rewardCoins}   .    Total: {CoinManager.TotalCoins}");
        }
    }
}
