using UnityEngine;

public class PanelView : MonoBehaviour
{
    [SerializeField] private PanelType panelType;
    [SerializeField] private bool active;
    public PanelType PanelType => panelType;
    public bool Active => active;
}