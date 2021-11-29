using System.Collections.Generic;
public class Controllers : IAwaker, IStarter, IUpdater
{
    private readonly List<IAwaker> controllerAwakers;
    private readonly List<IStarter> controllerStarters;
    private readonly List<IUpdater> controllerUpdaters;

    internal Controllers()
    {
        controllerAwakers = new List<IAwaker>();
        controllerStarters = new List<IStarter>();
        controllerUpdaters = new List<IUpdater>();
    }

    internal Controllers Add(IController controller)
    {
        if (controller is IAwaker controllerAwaker)
        {
            controllerAwakers.Add(controllerAwaker);
        }
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
    
    public void Awaker()
    {
        foreach (IAwaker awaker in controllerAwakers)
        {
            awaker.Awaker();
        }
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
