using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private float scoreMultiplier = 1f;
    [SerializeField] private int scoreParaGanar = 400;
    [SerializeField] private float distanciaEntreFrenesi = 150f;
    [SerializeField] private float duracionFrenesi = 5f;
    [SerializeField] private float multiplicadorScoreFrenesi = 2f;

    public TextMeshProUGUI scoreText;

    public int FinalScore { get; private set; }
    public bool Nivel2 { get; private set; }

    private bool partidaTerminada = false;
    private float proximoFrenesi;

    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        proximoFrenesi = distanciaEntreFrenesi;
    }

    void Update()
    {
        float factorFrenesi = GameManager.Instance.FrenesiActivo ? multiplicadorScoreFrenesi : 1f;
        AddScore(1 * Time.deltaTime * scoreMultiplier * factorFrenesi);
        scoreMultiplier += 0.001f;
        //salto a nivel 2, quizas con los cambios sea algo pronto, vere de cambiarlo
        if (score > 100 && !Nivel2)
        {
            Nivel2 = true;
        }

        if (FinalScore >= proximoFrenesi)
        {
            proximoFrenesi += distanciaEntreFrenesi;
            GameManager.Instance.ActivarFrenesi(duracionFrenesi);
        }

        if (FinalScore >= scoreParaGanar && !partidaTerminada)
        {
            partidaTerminada = true;
            SaveScore();
            GameManager.Instance.Ganar();
        }
    }
    private void AddScore(float points)
    {
        score += points;
        FinalScore = Mathf.FloorToInt(score);
        scoreText.text = "Score: " + FinalScore.ToString();
    }
    //guardado en playerprefs, no es seguro pero funciona bien en web
    public void SaveScore()
    {
        PlayerPrefs.SetInt("FinalScore", FinalScore);

        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (FinalScore > highscore)
        {
            PlayerPrefs.SetInt("Highscore", highscore = FinalScore);
        }

        PlayerPrefs.Save();
    }
}