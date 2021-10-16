using UnityEngine;

public class EnvironmentView : MonoBehaviour
{
    [SerializeField] private int damage = 0;
    public event System.Action<EnvironmentView, Collider2D> OnEnter;

    public int Damage => damage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(this, collision);
    }
}
