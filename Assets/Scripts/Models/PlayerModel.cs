using UnityEngine;

public class PlayerModel : MonoBehaviour // подскажите как выставлять параметры в инспекторе для модели, но не использовать моно для модели
{
    [SerializeField] private int health = 100;
    public int lifes = 3;
    public float pushForce = 5f;
    public float jumpForce = 4f;
    public event System.Action<PanelType> PanelView;

    public int Health
    {
        get => health;
        set
        {
            if (value > 0)
            {
                health = value;
            }
            else
            {
                health = 0;
                PanelView?.Invoke(PanelType.LevelLose);
            }
        }
    }
}
