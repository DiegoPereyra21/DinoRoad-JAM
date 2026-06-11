using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float score = 0;
    public TextMeshProUGUI scoreText;
    [SerializeField] private float scoreMultiplier = 1f;
    public int finalScore;
    public bool nivel2 = false;
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void Update()
    {
        addscore(1 * Time.deltaTime * scoreMultiplier);
        scoreMultiplier += 0.001f;
        if (score > 100 && nivel2 == false)
        {
            nivel2 = true;
        }
    }
    public void addscore(float points)
    {
        score += points;
        finalScore = Mathf.FloorToInt(score);
        scoreText.text = "Score: " + finalScore.ToString();
    }

    public void saveScore()
    {
        PlayerPrefs.SetInt("FinalScore", finalScore);

        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (finalScore > highscore)
        {
            PlayerPrefs.SetInt("Highscore", finalScore);
        }

        PlayerPrefs.Save();
    }
}
