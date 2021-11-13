public interface IAnalytics
{
    public void SendMessage(string nameEvent);
    public void SendMessage(string nameEvent, (string, object) data);
}