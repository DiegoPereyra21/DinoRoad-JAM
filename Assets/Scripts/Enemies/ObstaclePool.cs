using System.Collections.Generic;
using UnityEngine;

//reutiliza instancia en vez de destruirlas y instanciarlas de nuevo
public class ObstaclePool
{
    private readonly Dictionary<GameObject, Queue<GameObject>> disponiblesPorPrefab = new();
    private readonly Dictionary<GameObject, GameObject> prefabDeInstancia = new();
    public GameObject Obtener(GameObject prefab, Vector3 posicion)
    {
        if (!disponiblesPorPrefab.TryGetValue(prefab, out Queue<GameObject> disponibles))
        {
            disponibles = new Queue<GameObject>();
            disponiblesPorPrefab[prefab] = disponibles;
        }

        GameObject instancia;
        if (disponibles.Count > 0)
        {
            instancia = disponibles.Dequeue();
        }
        else
        {
            instancia = Object.Instantiate(prefab);
            prefabDeInstancia[instancia] = prefab;
        }

        instancia.transform.position = posicion;
        instancia.SetActive(true);
        return instancia;
    }
    public void Devolver(GameObject instancia)
    {
        instancia.SetActive(false);
        if (prefabDeInstancia.TryGetValue(instancia, out GameObject prefabOrigen))
        {
            disponiblesPorPrefab[prefabOrigen].Enqueue(instancia);
        }
    }
}