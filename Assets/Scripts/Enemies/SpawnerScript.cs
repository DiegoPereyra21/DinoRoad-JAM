using System.Collections;
using UnityEngine;
//refactorizado para tener el spawner tanto de los enemgios basicos como nivel 2
public class SpawnerScript : MonoBehaviour
{
    //spawners
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] obstacleNivel2Prefabs;
    [SerializeField] private float speedObstacle = 5f;
    public float obstacleSpawnInterval = 2f;
    private float timeUntilNextSpawn;
    private float increase = 0.085f;
    //puntajes
    [SerializeField] private GameObject scoreObject;
    private Score scoreScript;
    //frenesi
    [SerializeField] private float multiplicadorVelocidadFrenesi = 1.5f;

    private readonly ObstaclePool pool = new ObstaclePool();

    private void Start()
    {
        scoreScript = scoreObject.GetComponent<Score>();
    }
    private void Update()
    {
        speedObstacle += increase * Time.deltaTime;
        SpawnLoop();
    }
    //temposizador para q spawneen enemgios
    private void SpawnLoop()
    {
        timeUntilNextSpawn += Time.deltaTime;
        if (timeUntilNextSpawn >= obstacleSpawnInterval)
        {
            SpawnObstacle();
            timeUntilNextSpawn = 0f;
        }
    }
    //spawner
    private void SpawnObstacle()
    {
        GameObject[] prefabsDisponibles = scoreScript.Nivel2 ? obstacleNivel2Prefabs : obstaclePrefabs;
        GameObject prefabElegido = prefabsDisponibles[Random.Range(0, prefabsDisponibles.Length)];

        GameObject obstacle = pool.Obtener(prefabElegido, transform.position);

        float velocidadFinal = GameManager.Instance.FrenesiActivo
            ? speedObstacle * multiplicadorVelocidadFrenesi
            : speedObstacle;

        if (obstacle.TryGetComponent<IObstaculoMovible>(out var movible))
        {
            movible.ConfigurarVelocidad(velocidadFinal);
        }

        StartCoroutine(DevolverDespuesDe(obstacle, 5f));
    }
    private IEnumerator DevolverDespuesDe(GameObject obstaculo, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        pool.Devolver(obstaculo);
    }

    public void ApplyVelocidadMultiplicador(float multiplicador)
    {
        speedObstacle *= multiplicador;
    }
}