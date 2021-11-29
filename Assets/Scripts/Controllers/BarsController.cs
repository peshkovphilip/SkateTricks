using System.Collections.Generic;
using UnityEngine;

public class BarsController : IStarter
{
    private BarCoinsView barCoinsView;
    //private GameParams _gameParams;

    // public BarsController(GameParams gameParams)
    // {
    //     _gameParams = gameParams;
    // }
    public void Starter()
    {
        Debug.Log("start BarsController");
        barCoinsView = Object.FindObjectOfType<BarCoinsView>(true);
        //_gameParams.BarsUpdate += BarsUpdate;
    }

    // private void BarsUpdate(BarType barType)
    // {
    //     if (barType == BarType.Coin)
    //     {
    //         barCoinsView.ValueCoins = _gameParams.Coins;
    //     }
    // }
}
