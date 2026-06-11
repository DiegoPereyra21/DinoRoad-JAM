using UnityEngine;

// Mueve el layer hacia la izquierda y lo resetea cada loopWidth
// para simular un fondo infinito. Cada layer usa una velocidad distinta.
public class Background : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float loopWidth = 20f; // ancho de una repeticion del fondo

    private Vector3 startPosition;
    private float traveledDistance;

    private void Start()
    {
        // guardo la posicion inicial para usarla como referencia del loop
        startPosition = transform.position;
    }

    private void Update()
    {
        traveledDistance += scrollSpeed * Time.deltaTime;

        // Repeat hace que vuelva a 0 al pasar por el loop,
        // asi el layer salta a su posicion original sin que se note
        float offset = Mathf.Repeat(traveledDistance, loopWidth);
        transform.position = startPosition + Vector3.left * offset;
    }
}