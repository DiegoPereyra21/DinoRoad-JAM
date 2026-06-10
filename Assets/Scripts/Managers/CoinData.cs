using UnityEngine;
//sismte para que se guarde la cantidad de coins aunque reinicies la pagina usando PlayerPrefs
//tambien permite acceso a la informacion desde cualquier script
public class CoinManager : MonoBehaviour
{
    //cantidad total de monedas y al modificarse guarda automaticamente en el almacen local
    public static int TotalCoins
    {
        get => PlayerPrefs.GetInt("TotalCoins", 0);
        set
        {
            PlayerPrefs.SetInt("TotalCoins", value);
            PlayerPrefs.Save();
        }
    }
    /*
     * luego usaria esto para extraer la cantidad total de coins 
     * "int monedasActuales = CoinManager.TotalCoins;"
     * --------------------------
     * y esto para sumarle coins
     * "CoinManager.TotalCoins += 1";
     */
}
