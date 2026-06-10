using UnityEngine;

public class PlyerCollisionScript : MonoBehaviour
{
    //si choca contra un obstaculo, se destruye el player y se pasa a la escena de muerte
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            Destroy(gameObject);
            Debug.Log("You Die");
        }
    }
}
