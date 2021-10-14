using System.Collections.Generic;
public class Controllers : IStarter, IUpdater
{
    private readonly List<IStarter> controllerStarters;
    private readonly List<IUpdater> controllerUpdaters;

    internal Controllers()
    {
        controllerStarters = new List<IStarter>();
        controllerUpdaters = new List<IUpdater>();
    }

    internal Controllers Add(IController controller)
    {
        if (controller is IStarter controllerStarter)
        {
            controllerStarters.Add(controllerStarter);
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

    public void Updater()
    {
        foreach (IUpdater updater in controllerUpdaters)
        {
            updater.Updater();
        }
    }
}
