using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelsController : IStarter
{
    private List<PanelView> panels = new List<PanelView>();
    private PlayerModel playerModel;
    private GameParams gameParam;
    private UICanvasView uiCanvasView;

    public PanelsController(UICanvasView uiCanvasView)
    {
        this.uiCanvasView = uiCanvasView;
    }
    
    public void Starter()
    {
        Debug.Log("start PanelsController");

        gameParam = Object.FindObjectOfType<GameParams>();
        playerModel = Object.FindObjectOfType<PlayerModel>(true);
        panels = Object.FindObjectsOfType<PanelView>(true).ToList();

        foreach (PanelView panel in panels)
        {
            panel.gameObject.SetActive(panel.Active);
            gameParam.PanelView += ViewPanel;
            playerModel.PanelView += ViewPanel;
        }
    }

    private void ViewPanel(PanelType panelType)
    {
        panels.Find(x => x.PanelType == panelType).gameObject.SetActive(true);
        if (panelType == PanelType.LevelDone)
        {
            GameAnalytics.SendMessage("level_done");
        }
        if (panelType == PanelType.LevelLose)
        {
            GameAnalytics.SendMessage("level_lose");
        }

        if ((panelType == PanelType.LevelDone) || (panelType == PanelType.LevelLose))
        {
            uiCanvasView.Panels[(int)PanelType.Inventory].gameObject.SetActive(false);
        }
    }

    public List<PanelView> GetPanels()
    {
        return panels;
    }
}
