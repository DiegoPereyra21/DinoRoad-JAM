using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    public Score scriptScore;

    //colision con obstactulo = perder
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            scriptScore.SaveScore();
            GetComponent<PlayerDeath>().Die();
        }
    }

    //trigger q cuenta las monedas, score y demas
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IRecolectable>(out var recolectable))
        {
            recolectable.Recolectar();
        }
    }
}