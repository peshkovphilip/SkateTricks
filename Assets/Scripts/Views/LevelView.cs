using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Transform[] linesPosition;
    [SerializeField] private LevelLines startLine;
    [SerializeField] private Transform spawnPoint;

    public Transform[] LinePositions => linesPosition;
    public LevelLines StartLine => startLine;
    public Transform SpawnPoint => spawnPoint;
}
