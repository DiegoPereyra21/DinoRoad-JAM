using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //array con los prefabs de los obstaculos
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] obstacleNivel2Prefabs;
    public float obstacleSpawnInterval = 2f;
    private float timeUntilNextSpawn;
    private float increase= 0.085f;

    [SerializeField] private GameObject scoreObject;
    private Score scoreScript;

    [SerializeField] private float speedObstacle = 5f;


    private void Start()
    {
        scoreScript = scoreObject.GetComponent<Score>();
    }
    private void Update()
    {
        speedObstacle += increase * Time.deltaTime;
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        //Temporizador entre cada spawn de obstaculo
        timeUntilNextSpawn += Time.deltaTime;
        if (timeUntilNextSpawn >= obstacleSpawnInterval)
        {
            SpawnObstacle();
            timeUntilNextSpawn = 0f;
        }
    }

    private void SpawnObstacle()
    {
        if(scoreScript.nivel2 == false)
        {
            //elige aleatoriamente un prefab del array y lo instancia en la posicion del spawner con su misma rotacion
            GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            GameObject obstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
            //le doy movimiento perpetuo hacia la izquierda al obstaculo con una respectiva velocidad
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.left * speedObstacle;
            }
            Destroy(obstacle, 5f);
        }
        else
        {
            //elige aleatoriamente un prefab del array y lo instancia en la posicion del spawner con su misma rotacion
            GameObject obstacleToSpawn = obstacleNivel2Prefabs[Random.Range(0, obstacleNivel2Prefabs.Length)];
            GameObject obstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
            //le doy movimiento perpetuo hacia la izquierda al obstaculo con una respectiva velocidad
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.left * speedObstacle;
            }
            Destroy(obstacle, 5f);
        }
        
    }

}
