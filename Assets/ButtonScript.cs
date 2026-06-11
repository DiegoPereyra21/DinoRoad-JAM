using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public Button buttonBuy;
    public Button buttonEquip;

    public int costCharacter = 15;

    public void checkCoins()
    {
        if (CoinManager.TotalCoins >= costCharacter)
        {
            CoinManager.TotalCoins -= 15;
            BuyCharacter();
            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No te alcanza");
        }
    }


    public void BuyCharacter()
    {
        Debug.Log("Character bought");
    }
}
