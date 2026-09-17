using UnityEngine;
using UnityEngine.UI;
using TMPro;
//ahora esto maneja mas que todo el tema de la tienda, quite el otro script que tenia antes
public class ButtonScript : MonoBehaviour
{
    public Button buttonBuy;
    public Button buttonEquip;

    public TextMeshProUGUI textButtonEquip;
    public int dinoIndex;
    public int costCharacter;
    [SerializeField] private GameOverManager gameOverManager;

    private void Start()
    {
        if (IsUnlocked())
        {
            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);
            updateButtonText();
        }
        else
        {
            buttonBuy.gameObject.SetActive(true);
            buttonEquip.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        DinoEquipoEventos.OnDinoEquipado += ManejarCambioDeEquipo;
    }

    private void OnDisable()
    {
        DinoEquipoEventos.OnDinoEquipado -= ManejarCambioDeEquipo;
    }

    private void ManejarCambioDeEquipo(int indiceEquipado)
    {
        updateButtonText();
    }

    public void checkCoins()
    {
        if (IsUnlocked()) return;

        if (CoinManager.TotalCoins >= costCharacter)
        {
            CoinManager.TotalCoins -= costCharacter;
            gameOverManager.UpdatePoints();
            UnlockCharacter();

            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);

            if (textButtonEquip != null) textButtonEquip.text = "Equip";
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
    //todos los guardados con PlayerPrefs para que ande en Web
    private bool IsUnlocked()
    {
        return PlayerPrefs.GetInt("Unlocked_" + dinoIndex, 0) == 1;
    }

    private void UnlockCharacter()
    {
        PlayerPrefs.SetInt("Unlocked_" + dinoIndex, 1);
        PlayerPrefs.Save();
    }

    public void EquipCharacter()
    {
        int equipadoActual = PlayerPrefs.GetInt("EquippedDinoIndex", 0);
        int nuevoEquipado = (equipadoActual == dinoIndex) ? 0 : dinoIndex;

        PlayerPrefs.SetInt("EquippedDinoIndex", nuevoEquipado);
        PlayerPrefs.Save();

        DinoEquipoEventos.Notificar(nuevoEquipado);
    }

    private void updateButtonText()
    {
        if (textButtonEquip == null) { return; }
        if (PlayerPrefs.GetInt("EquippedDinoIndex", 0) == dinoIndex)
        {
            textButtonEquip.text = "Unequip";
        }
        else
        {
            textButtonEquip.text = "Equip";
        }
    }
}