using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //aviso de frenesi, luego hacerlo mas lindo
    [SerializeField] private GameObject frenesiUI;

    public static GameManager Instance { get; private set; }

    public bool FrenesiActivo { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ActivarFrenesi(float duracion)
    {
        StartCoroutine(FrenesiCoroutine(duracion));
    }

    private IEnumerator FrenesiCoroutine(float duracion)
    {
        FrenesiActivo = true;
        Debug.Log("Frenesi ON. frenesiUI = " + frenesiUI);
        if (frenesiUI != null) frenesiUI.SetActive(true);

        yield return new WaitForSeconds(duracion);

        FrenesiActivo = false;
        if (frenesiUI != null) frenesiUI.SetActive(false);
    }

    public void Ganar() => TerminarPartida(new Victoria());
    public void Perder() => TerminarPartida(new Derrota());

    private void TerminarPartida(ResultadoPartida resultado)
    {
        SceneManager.LoadScene(resultado.NombreEscena);
    }
}