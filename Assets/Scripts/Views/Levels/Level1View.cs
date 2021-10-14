using UnityEngine;

public class Level1View : MonoBehaviour
{
    [SerializeField] private Transform[] linesPosition;
    [SerializeField] private LevelLines startLine;

    public Transform[] LinePositions
    {
        get => linesPosition;
    }

    public LevelLines StartLine
    {
        get => startLine;
    }
}
