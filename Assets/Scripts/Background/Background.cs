using UnityEngine;

// Mueve el layer hacia la izquierda y lo resetea al ancho de una repeticion
// para que el fondo parezca infinito. Va en el PADRE del layer, no en las copias.
public class Background : MonoBehaviour
{
    public float scrollSpeed = 1f;

    Vector3 startPosition;
    float loopWidth;
    float traveledDistance;

    void Start()
    {
        startPosition = transform.position;
        loopWidth = CalculateLoopWidth();
    }

    void Update()
    {
        traveledDistance += scrollSpeed * Time.deltaTime;

        // Repeat hace que el offset vuelva a 0 al pasar loopWidth,
        // asi el layer salta a su posicion inicial sin que se note
        float offset = Mathf.Repeat(traveledDistance, loopWidth);
        transform.position = startPosition + Vector3.left * offset;
    }

    // el periodo del loop es la distancia entre una copia y la siguiente,
    // lo mido directo de la escena asi no dependo de valores tipeados a mano
    float CalculateLoopWidth()
    {
        SpriteRenderer[] copies = GetComponentsInChildren<SpriteRenderer>();

        if (copies.Length < 2)
            return copies[0].bounds.size.x;

        // busco las dos copias mas a la izquierda y uso su separacion en x
        float first = float.MaxValue;
        float second = float.MaxValue;

        foreach (SpriteRenderer copy in copies)
        {
            float x = copy.transform.position.x;
            if (x < first)
            {
                second = first;
                first = x;
            }
            else if (x < second)
            {
                second = x;
            }
        }

        return second - first;
    }
}