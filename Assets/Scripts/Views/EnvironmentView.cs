using UnityEngine;

public class EnvironmentView : MonoBehaviour
{
    [SerializeField] private int _damage = 100;
    [SerializeField] private EEnvironmentType _type;
    public event System.Action<EnvironmentView, Collider2D> OnEnter;

    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }

    public EEnvironmentType Type => _type;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(this, collision);
    }
}
