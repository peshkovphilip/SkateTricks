using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelsController : IStarter
{
    private List<PanelView> panels = new List<PanelView>();
    private PlayerModel playerModel;
    private GameParams gameParam;

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
    }

    public List<PanelView> GetPanels()
    {
        return panels;
    }
}
