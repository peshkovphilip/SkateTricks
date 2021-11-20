using UnityEngine;

public class PlayerBodyView : MonoBehaviour
{
    [SerializeField] public Transform petPosition;
    [SerializeField] private SpriteRenderer jetPackSprite;

    public SpriteRenderer JetPackSprite => jetPackSprite;
}
