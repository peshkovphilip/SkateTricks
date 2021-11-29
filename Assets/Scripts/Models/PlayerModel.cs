using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int lifes = 3;
    [SerializeField] private float pushForce = 5f;
    [SerializeField] private float jumpForce = 4f;
    //private 
    public event System.Action<PanelType> PanelView;
    public event System.Action<int> SetHealth;

    public int Health
    {
        get => health;
        set
        {
            if (value > 0)
            {
                health = value;
                SetHealth?.Invoke(health);
            }
            else
            {
                health = 0;
                PanelView?.Invoke(PanelType.LevelLose);
            }
        }
    }

    public int Lifes
    {
        get => lifes; 
        set => lifes = value;
    }

    public float PushForce
    {
        get => pushForce; 
        set => pushForce = value;
    }

    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = value;
    }
}
