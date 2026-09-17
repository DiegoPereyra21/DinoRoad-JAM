using UnityEngine;

public static class CoinManager
{
    public static int TotalCoins
    {
        get => PlayerPrefs.GetInt("TotalCoins", 0);
        set
        {
            PlayerPrefs.SetInt("TotalCoins", value);
            PlayerPrefs.Save();
        }
    }
}