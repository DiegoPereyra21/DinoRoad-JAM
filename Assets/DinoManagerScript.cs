using UnityEngine;

public class DinoManagerScript : MonoBehaviour
{
    public GameObject[] dinosaurios;

    [Header("Referencias para aplicar el bonus de velocidad del personaje equipado")]
    [SerializeField] private SpawnerScript spawner;
    [SerializeField] private Background[] fondos;

    private readonly PersonajeBase[] personajes = { new Dino(), new Dino1(), new Dino2(), new Dino3() };

    void Start()
    {
        int indiceEquipado = PlayerPrefs.GetInt("EquippedDinoIndex", 0);

        for (int i = 0; i < dinosaurios.Length; i++)
        {
            dinosaurios[i].SetActive(i == indiceEquipado);
        }

        AplicarStats(dinosaurios[indiceEquipado], personajes[indiceEquipado]);
    }

    private void AplicarStats(GameObject dino, PersonajeBase personaje)
    {
        PlayerMovement movement = dino.GetComponent<PlayerMovement>();
        if (movement != null) movement.jumpForce = personaje.Salto;

        if (spawner != null) spawner.ApplyVelocidadMultiplicador(personaje.MultiplicadorVelocidad);

        foreach (Background fondo in fondos)
        {
            if (fondo != null) fondo.scrollSpeed *= personaje.MultiplicadorVelocidad;
        }
    }
}