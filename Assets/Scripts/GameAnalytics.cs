using UnityEngine.Analytics;
using System.Collections.Generic;

public static class GameAnalytics
{
    public static void SendMessage(string nameEvent)
    {
        Analytics.CustomEvent(nameEvent);
    }

    public static void SendMessage(string nameEvent, (string, object) data)
    {
        var eventData = new Dictionary<string, object> { [data.Item1] = data.Item2 };
        Analytics.CustomEvent(nameEvent, eventData);
    }
}