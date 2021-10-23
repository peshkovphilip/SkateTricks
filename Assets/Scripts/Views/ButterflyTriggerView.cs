using System;
using UnityEngine;

public class ButterflyTriggerView : MonoBehaviour
{
    [SerializeField] private GameObject butterfly;

    public Action<GameObject, GameObject> TriggerEnter;
    public Action<GameObject, GameObject> TriggerExit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEnter?.Invoke(collision.gameObject, butterfly);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerExit?.Invoke(collision.gameObject, butterfly);
    }
}
