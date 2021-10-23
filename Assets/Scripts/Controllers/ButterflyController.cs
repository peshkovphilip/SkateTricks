using UnityEngine;

public class ButterflyController : IStarter
{
    private ButterflyTriggerView[] butterflyTriggerViews;

    public void Starter()
    {
        Debug.Log("start ButterflyController");
        butterflyTriggerViews = Object.FindObjectsOfType<ButterflyTriggerView>();
        for (int i = 0; i < butterflyTriggerViews.Length; i++)
        {
            butterflyTriggerViews[i].TriggerEnter += MoveToTarget;
            butterflyTriggerViews[i].TriggerExit += MoveToBase;
        }
    }

    private void MoveToTarget(GameObject collider, GameObject butterfly)
    {
        Debug.Log("collider name ="+collider.name);
        if (collider.GetComponent<PlayerBodyView>() != null)
        {
            butterfly.GetComponent<Pathfinding.AIDestinationSetter>().target = collider.GetComponent<PlayerBodyView>().petPosition; // можно здесь как-то избавиться от GetComponent ?
        }
    }
    private void MoveToBase(GameObject collider, GameObject butterfly)
    {
        if (collider.GetComponent<PlayerBodyView>() != null)
        {
            butterfly.GetComponent<Pathfinding.AIDestinationSetter>().target = butterfly.GetComponent<ButterflyView>().basePosition;
        }
    }
}
