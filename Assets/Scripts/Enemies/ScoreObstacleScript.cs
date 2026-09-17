using UnityEngine;
//recompensa x cada obstaculo
public class ScoreObstacleScript : MonoBehaviour, IRecolectable
{
    public int rewardCoins = 1;
    public void Recolectar()
    {
        CoinManager.TotalCoins += rewardCoins;
    }
}