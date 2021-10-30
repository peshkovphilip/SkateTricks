using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Controllers controllers;

    private void Start()
    {
        controllers = new Controllers();
        controllers.Add(new PlayerController());
        controllers.Add(new BarsController());
        controllers.Add(new PanelsController());
        controllers.Add(new ButterflyController());
        InventoryController inventoryController = new InventoryController();
        controllers.Add(inventoryController);
        controllers.Add(new QuestController(inventoryController.Inventory));
        controllers.Starter();
    }

    private void Update()
    {
        controllers.Updater();
    }
}
