using System.Linq;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Controllers controllers;
    private UICanvasView uiCanvasView;
    private PlayerView playerView;
    private PlayerModel playerModel;

    private void Start()
    {
        uiCanvasView = FindObjectOfType<UICanvasView>();
        playerView = FindObjectOfType<PlayerView>(true); 
        playerModel = FindObjectOfType<PlayerModel>(true); 
        
        controllers = new Controllers();
        controllers.Add(new PlayerController(playerView, playerModel));
        controllers.Add(new PanelsController(uiCanvasView));
        controllers.Add(new UIController(uiCanvasView.Panels.ToList()));
        controllers.Add(new ButterflyController());

        AbilityController abilityController = new AbilityController(playerModel);
        controllers.Add(abilityController);
        
        InventoryController inventoryController = new InventoryController(uiCanvasView, playerView, abilityController.GetAbilities());
        controllers.Add(inventoryController);
        
        controllers.Add(new QuestController(inventoryController.Inventory));
        controllers.Add(new AdsController());
        controllers.Starter();
    }

    private void Update()
    {
        controllers.Updater();
    }
}
