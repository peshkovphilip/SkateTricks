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
        controllers.Starter();
    }

    private void Update()
    {
        controllers.Updater();
    }
}
