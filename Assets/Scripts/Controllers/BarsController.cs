using UnityEngine;

public class BarsController : IStarter
{
    private BarCoinsView barCoinsView;
    private GameParams gameParams;

    public void Starter()
    {
        Debug.Log("start BarsController");
        gameParams = Object.FindObjectOfType<GameParams>();
        barCoinsView = Object.FindObjectOfType<BarCoinsView>();
        gameParams.BarsUpdate += BarsUpdate;
    }

    private void BarsUpdate(BarType barType)
    {
        if (barType == BarType.Coin)
        {
            barCoinsView.ValueCoins = gameParams.Coins;
        }
    }
}
