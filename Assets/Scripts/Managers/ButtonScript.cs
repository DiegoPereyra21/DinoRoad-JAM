using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public Button buttonBuy;
    public Button buttonEquip;
    //id para cada personaje
    public string characterID;
    public int costCharacter = 15;

    private void Start()
    {
        //al entrar en escena vemos que boton debe estar activo segun si esta desbloqueado el personaje o no
        if (IsUnlocked())
        {
            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);
        }
        else
        {
            buttonBuy.gameObject.SetActive(true);
            buttonEquip.gameObject.SetActive(false);
        }
    }


    public void checkCoins()
    {
        //En caso de ya estar desbloqueado directamente sale
        if (IsUnlocked()) return;

        if (CoinManager.TotalCoins >= costCharacter)
        {
            CoinManager.TotalCoins -= 15;

            UnlockCharacter();
            BuyCharacter();

            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }


    public void BuyCharacter()
    {
        Debug.Log("Character bought" + characterID);
    }

    private bool IsUnlocked()
    {
        //verificacion sobre si esta guardado como bloqueado o no, 0 bloqueado y 1 desbloqueado
        return PlayerPrefs.GetInt("Unlocked_" + characterID, 0) == 1;
    }

    private void UnlockCharacter()
    {
        //una vez comprado, llama a esta funcion para que asigne desbloqueado al personaje
        PlayerPrefs.SetInt("Unlocked_" + characterID, 1);
        PlayerPrefs.Save();
    }

}
