using UnityEngine;
public class Pause : MonoBehaviour
{
    public GameObject pausePanel; //Esto va al canva
    private bool gamePause = false;


    void Start()
    {
        pausePanel.SetActive(false); //Inicializa oculto
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.K))
        {
            if (gamePause == false)
            {
                pauseGame();
            }
            else
            {
                continueGame();
            }
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true); //Activar el menu de pausa
        gamePause = true;
    }

    void continueGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false); //Oculta el menu de pausa
        gamePause = false;
    }
}