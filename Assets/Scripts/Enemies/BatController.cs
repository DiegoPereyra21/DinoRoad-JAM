using UnityEngine;

//zigzag del murcielago
public class EnemyMovement : MonoBehaviour
{
    float baseY;   //altura base del prefab
    float timer;
    float amplitude;
    float phase;

    void Awake()
    {
        baseY = transform.localPosition.y;
    }

    void OnEnable()
    {
        //randomiza
        timer = 0f;
        amplitude = Random.Range(0.3f, 1.2f);
        phase = Random.Range(0f, Mathf.PI * 2f);
    }
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 pos = transform.localPosition;
        pos.y = baseY + Mathf.Sin(timer * 3f + phase) * amplitude;
        transform.localPosition = pos;
    }
}