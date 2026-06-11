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
    public int costCharacter = 15;



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
            CoinManager.TotalCoins -= 15;

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
            //mensaje de dino desequipado
        }else
        {
            //si era otro dino, lo equipa
            PlayerPrefs.SetInt("EquippedDinoIndex", dinoIndex);
        }
        // Guardamos el número de dino seleccionado
        PlayerPrefs.Save();
        //mensaje o alguna forma de mostrar que esta equipado
        Debug.Log("Equipado el dino: " + dinoIndex);
        //cambiamos el mensaje del boton automaticamente luego del click
        updateButtonText();
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
