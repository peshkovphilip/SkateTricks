using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasView : MonoBehaviour
{
    [SerializeField] private PanelView[] panels;

    public PanelView[] Panels => panels;
}
