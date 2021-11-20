using UnityEngine;
using UnityEngine.UI;

public class BarCoinsView : MonoBehaviour
{
    [SerializeField] private Text valueCoins;

    public int ValueCoins
    {
        set => valueCoins.text = value.ToString();
    }
}
