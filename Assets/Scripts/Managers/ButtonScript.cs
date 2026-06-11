using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public Button buttonBuy;
    public Button buttonEquip;

    //creo que es la forma mas facil de cambiar el texto es con esta referencia al equip, podria haber hecho lo mismo con el boton de buy pero era complicarse
    public TextMeshProUGUI textButtonEquip;
    //id para cada personaje
    public int dinoIndex;
    //costo base que la idea es cambiarlo en cada dino
    public int costCharacter;
    //para actualizar los puntos en el menu
    [SerializeField] private GameOverManager gameOverManager;


    private void Start()
    {
        //al entrar en escena vemos que boton debe estar activo segun si esta desbloqueado el personaje o no
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


    //funcion que verifica si te alcanza para un dino nuevo o no
    public void checkCoins()
    {
        //En caso de ya estar desbloqueado directamente sale
        if (IsUnlocked()) return;

        if (CoinManager.TotalCoins >= costCharacter)
        {
            CoinManager.TotalCoins -= costCharacter;
            gameOverManager.UpdatePoints();
            UnlockCharacter();

            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);

            //una vez comprado ya el texto por defecto es "Equip"
            if (textButtonEquip != null) textButtonEquip.text = "Equip";
        }
        else
        {
            //Mostrar mensaje de que te faltan coins o algun efecto
            Debug.Log("Not enough coins");
        }
    }


    private bool IsUnlocked()
    {
        //verificacion sobre si esta guardado como bloqueado o no, 0 bloqueado y 1 desbloqueado
        return PlayerPrefs.GetInt("Unlocked_" + dinoIndex, 0) == 1;
    }


    private void UnlockCharacter()
    {
        //una vez comprado, llama a esta funcion para que asigne desbloqueado al personaje
        PlayerPrefs.SetInt("Unlocked_" + dinoIndex, 1);
        PlayerPrefs.Save();
    }


    public void EquipCharacter()
    {
        //verificamos cual esta equipado para saber si equipar o desequipar el dino
        if(PlayerPrefs.GetInt("EquippedDinoIndex", 0) == dinoIndex)
        {
            PlayerPrefs.SetInt("EquippedDinoIndex", 0);
            //ESTO ME FALTABA, porque no entendia porque me aparecia unequip en mas de un dino
            updateButtonText();
        }else
        {
            //si era otro dino, lo equipa
            PlayerPrefs.SetInt("EquippedDinoIndex", dinoIndex);
            //esto me habia olvidado, completamente necesario para avisarle antes a los demas
            PlayerPrefs.Save();


            //No es la mejor practica ni de lejos, pero prefiero esto antes que tener 3 referencias mas a botones en este script
            ButtonScript[] todosLosBotones = FindObjectsByType<ButtonScript>(FindObjectsSortMode.None);

            //cada que equipa algo le avisa a los demas de que actualicen y vean que hubo un cambio
            foreach (ButtonScript boton in todosLosBotones)
            {
                boton.updateButtonText();
            }
        }

        //no me gusto la solucion pero es lo mejor para ser rapidos
        PlayerPrefs.Save();
    }


    //funcion que cambia el boton entre equipado o desequipado (faltaria algun efecto visual pero no creo que de tiempo)
    private void updateButtonText()
    {
        if(textButtonEquip == null) { return ; }
        //muy parecido al equipCharacter pero para el text del boton
        if(PlayerPrefs.GetInt("EquippedDinoIndex", 0) == dinoIndex)
        {
            textButtonEquip.text = "Unequip";
        }
        else
        {
            textButtonEquip.text = "Equip";
        }
    }


}
