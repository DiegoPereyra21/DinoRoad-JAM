using UnityEngine;

// Mueve al enemigo hacia la izquierda como los demas obstaculos.
// La amplitud del vuelo es random asi cada murcielago vuela distinto.
public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 4f; // tiene que ir parecido a la velocidad del ground

    float startY;
    float timer;
    float amplitude;
    float phase;

    void Start()
    {
        startY = transform.position.y;

        amplitude = Random.Range(0.3f, 1.2f);
        // arranco la onda en un punto random para que dos murcielagos
        // que spawneen juntos no aleteen sincronizados
        phase = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        float x = transform.position.x - moveSpeed * Time.deltaTime;
        float y = startY + Mathf.Sin(timer * 3f + phase) * amplitude;

        transform.position = new Vector3(x, y, transform.position.z);

        // cuando sale de la pantalla por la izquierda lo borro
        if (x < -15f)
            Destroy(gameObject);
    }
}