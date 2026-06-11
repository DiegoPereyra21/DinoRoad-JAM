using UnityEngine;

public class DinoManagerScript : MonoBehaviour
{
    //referencias a cada dino, aunque esten desactivados
    public GameObject[] dinosaurios;

    void Start()
    {
        //verifica que dino esta equipado, por defecto 0
        int indiceEquipado = PlayerPrefs.GetInt("EquippedDinoIndex", 0);

        //para recorrer la lista de dinos 
        for (int i = 0; i < dinosaurios.Length; i++)
        {
            //verifica en cada caso si el indice coincide
            dinosaurios[i].SetActive(i == indiceEquipado);
        }
    }
}
