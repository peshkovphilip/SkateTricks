using System.Collections.Generic;
public class Controllers : IStarter, IDestroyer, IUpdater
{
    private readonly List<IStarter> controllerStarters;
    private readonly List<IDestroyer> controllerDestroyers;
    private readonly List<IUpdater> controllerUpdaters;

    internal Controllers()
    {
        controllerStarters = new List<IStarter>();
        controllerDestroyers = new List<IDestroyer>();
        controllerUpdaters = new List<IUpdater>();
    }

    internal Controllers Add(IController controller)
    {
        if (controller is IStarter controllerStarter)
        {
            controllerStarters.Add(controllerStarter);
        }
        if (controller is IDestroyer controllerDestroyer)
        {
            controllerDestroyers.Add(controllerDestroyer);
        }
        if (controller is IUpdater controllerUpdater)
        {
            controllerUpdaters.Add(controllerUpdater);
        }
        return this;
    }

    public void Starter()
    {
        foreach (IStarter starter in controllerStarters)
        {
            starter.Starter();
        }
    }

    public void Destroyer()
    {
        foreach (IDestroyer destroyer in controllerDestroyers)
        {
            destroyer.Destroyer();
        }
    }

    public void Updater()
    {
        foreach (IUpdater updater in controllerUpdaters)
        {
            updater.Updater();
        }
    }
}
