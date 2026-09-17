using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}