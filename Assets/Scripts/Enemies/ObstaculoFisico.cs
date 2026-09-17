using UnityEngine;

//aplica velocidad directamente al rigidbody
public class ObstaculoFisico : MonoBehaviour, IObstaculoMovible
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void ConfigurarVelocidad(float velocidadBase)
    {
        if (rb != null) rb.linearVelocity = Vector2.left * velocidadBase;
    }
}