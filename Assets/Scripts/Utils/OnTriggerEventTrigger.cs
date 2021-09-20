using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEventTrigger : MonoBehaviour
{
    [System.Serializable]
    private class ColliderEvent
    {
        public LayerMask layerMask;
        public UnityEvent<GameObject> unityEvent;
    }

    [SerializeField] private ColliderEvent[] _onTriggersEnter;
    [SerializeField] private ColliderEvent[] _onTriggersStay;
    [SerializeField] private ColliderEvent[] _onTriggersExit;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        foreach (ColliderEvent onTriggerEnter in _onTriggersEnter)
        {
            if (onTriggerEnter.layerMask.Contains(go.layer))
            {
                onTriggerEnter.unityEvent.Invoke(go);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject go = other.gameObject;

        foreach (ColliderEvent onTriggerStay in _onTriggersStay)
        {
            if (onTriggerStay.layerMask.Contains(go.layer))
            {
                onTriggerStay.unityEvent.Invoke(go);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;

        foreach (ColliderEvent onTriggerExit in _onTriggersExit)
        {
            if (onTriggerExit.layerMask.Contains(go.layer))
            {
                onTriggerExit.unityEvent.Invoke(go);
            }
        }
    }

}
