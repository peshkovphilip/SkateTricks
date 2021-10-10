using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Controllers controllers;

    private void Awake()
    {

    }
    private void Start()
    {
        controllers = new Controllers();
        controllers.Add(new PlayerController());
        controllers.Starter();
    }
    private void Update()
    {
        controllers.Updater();
    }
    private void FixedUpdate()
    {

    }
    private void OnDestroy()
    {
        controllers.Destroyer();
    }
}
