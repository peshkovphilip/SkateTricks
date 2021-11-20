using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] public SpriteRenderer skin;
    [SerializeField] public Rigidbody2D phisics;
    [SerializeField] public CapsuleCollider2D collider;
    [SerializeField] public Animator animator;
    [SerializeField] private PlayerBodyView body;

    public PlayerBodyView Body => body;
}
