using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI textFinalScore;
    public TextMeshProUGUI textHighscore;

    public TextMeshProUGUI textFinalCoin;
    void Start()
    {
        int FinalScore = PlayerPrefs.GetInt("FinalScore", 0);
        textFinalScore.text = "Score: " + FinalScore.ToString();

        int BestScore = PlayerPrefs.GetInt("Highscore", 0);
        textHighscore.text = "Max Score: " + BestScore.ToString();

        int TotalCoins = CoinManager.TotalCoins;
        textFinalCoin.text = "Coins: " + TotalCoins.ToString();

    }
}
