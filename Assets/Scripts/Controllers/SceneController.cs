using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SceneController : IStarter
{
    private List<PanelView> _panels;
    public SceneController(List<PanelView> panels)
    {
        _panels = panels;
    }
    public void Starter()
    {
        foreach (PanelView panelView in _panels.FindAll(x => x.PanelType != PanelType.Loading))
        {
            panelView.gameObject.SetActive(false);
        }
        _panels.Find(x => x.PanelType == PanelType.Loading).gameObject.SetActive(true);
        WaitingSprites();
        NotificationManager.CreateNotification(ENotificationType.Miss);
    }
    
    private async Task WaitingSprites()
    {
        await SpriteManager.InitAllSprites();
        
    }
}
