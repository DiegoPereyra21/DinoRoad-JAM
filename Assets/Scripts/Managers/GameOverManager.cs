using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI textFinalScore;
    public TextMeshProUGUI textHighscore;

    public TextMeshProUGUI textFinalCoin;
    void Start()
    {
        int puntajeFinal = PlayerPrefs.GetInt("FinalScore", 0);
        textFinalScore.text = "Score: " + puntajeFinal.ToString();

        int mejorPuntaje = PlayerPrefs.GetInt("Highscore", 0);
        textHighscore.text = "Max Score: " + mejorPuntaje.ToString();

        int coinTotal = CoinManager.TotalCoins;
        textFinalCoin.text = "Coins: " + coinTotal.ToString();

    }
}
