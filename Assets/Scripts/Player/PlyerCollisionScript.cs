using UnityEngine;
using UnityEngine.SceneManagement;

public class PlyerCollisionScript : MonoBehaviour
{

    public Score scriptScore;
    //si choca contra un obstaculo, se destruye el player y se pasa a la escena de muerte
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            scriptScore.saveScore();
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
