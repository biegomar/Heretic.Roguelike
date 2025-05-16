using System.Collections.Generic;

namespace Heretic.Roguelike.Daemons;

public class DaemonHandler
{
    private readonly List<IDaemon> observers = new(); 

    public void RegisterDaemon(IDaemon daemon)
    {
        observers.Add(daemon);
    }

    public void UnregisterDaemon(IDaemon daemon)
    {
        observers.Remove(daemon);
    }

    public void NotifyDaemons()
    {
        foreach (var observer in observers)
        {
            observer.Update();
        }
    }
}