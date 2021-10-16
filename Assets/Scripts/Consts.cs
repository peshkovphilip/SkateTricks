using UnityEngine;

public class Consts : MonoBehaviour
{
    [SerializeField] private float minPlayerMagnitudeForIdle = 0.2f;
    public float MinPlayerMagnitudeForIdle
    {
        get => minPlayerMagnitudeForIdle;
        set
        {
            if ((value >= 0) && (value < 1))
            {
                minPlayerMagnitudeForIdle = value;
            }
        }
    }

    public int DiffBetweenLayersAndLines { get => 6; private set { } }

}
