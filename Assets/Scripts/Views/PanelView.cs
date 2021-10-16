using UnityEngine;

public class PanelView : MonoBehaviour
{
    [SerializeField] private PanelType panelType;

    public PanelType PanelType => panelType;
}
