using UnityEngine;

public class PlyerCollisionScript : MonoBehaviour
{
    public Score scriptScore;

    // si choca contra un obstaculo, guarda el score y delega la muerte
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            scriptScore.saveScore();
            GetComponent<PlayerDeath>().Die();
        }
    }
}