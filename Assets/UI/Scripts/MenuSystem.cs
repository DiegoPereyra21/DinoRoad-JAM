using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuSystem : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

 
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
