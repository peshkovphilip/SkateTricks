using UnityEngine;

public class GameParams : MonoBehaviour
{
    [SerializeField] private int coins = 0;
    [SerializeField] private bool pause = false;
    private bool levelDone = false;
    public event System.Action<BarType> BarsUpdate;
    public event System.Action<PanelType> PanelView;

    public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            BarsUpdate?.Invoke(BarType.Coin);
        }
    }

    public bool Pause
    {
        get => pause;
        set
        {
            pause = value;
        }
    }

    public bool LevelDone
    {
        get => levelDone;
        set
        {
            levelDone = value;
            PanelView?.Invoke(PanelType.LevelDone);
        }
    }
}
