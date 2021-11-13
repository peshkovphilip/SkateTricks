using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Controllers controllers;

    private void Start()
    {
        controllers = new Controllers();
        controllers.Add(new PlayerController());
        controllers.Add(new BarsController());
        PanelsController panelController = new PanelsController();
        controllers.Add(panelController);
        panelController.Starter(); //как передать панели в UIController ?
        controllers.Add(new UIController(panelController.GetPanels()));
        controllers.Add(new ButterflyController());
        InventoryController inventoryController = new InventoryController();
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
