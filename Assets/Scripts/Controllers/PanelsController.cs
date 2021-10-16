using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsController : IStarter
{
    private PanelView[] panels;
    private PlayerModel playerModel;
    private GameParams gameParam;
    private GameObject panelLevelDone;
    private GameObject panelLevelLose;
    
    public void Starter()
    {
        Debug.Log("start PanelsController");

        gameParam = Object.FindObjectOfType<GameParams>();
        playerModel = Object.FindObjectOfType<PlayerModel>();
        panels = Object.FindObjectsOfType<PanelView>(true);
        foreach (PanelView panel in panels)
        {
            panel.gameObject.SetActive(false);
            if (panel.PanelType == PanelType.LevelDone) panelLevelDone = panel.gameObject;
            if (panel.PanelType == PanelType.LevelLose) panelLevelLose = panel.gameObject;
        }
        gameParam.PanelView += ViewPanel;
        playerModel.PanelView += ViewPanel;
    }

    private void ViewPanel(PanelType panelType)
    {
        if (panelType == PanelType.LevelDone)
        {
            panelLevelDone.SetActive(true);
        }
        if (panelType == PanelType.LevelLose)
        {
            panelLevelLose.SetActive(true);
        }
    }
}
