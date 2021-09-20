using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEventTrigger : MonoBehaviour
{
    [System.Serializable]
    private class CollisionEvent
    {
        public LayerMask layerMask;
        public UnityEvent<GameObject> unityEvent;
    }

    [SerializeField] private CollisionEvent[] _onCollisionsEnter;
    [SerializeField] private CollisionEvent[] _onCollisionsStay;
    [SerializeField] private CollisionEvent[] _onCollisionsExit;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;

        foreach (CollisionEvent onCollisionEnter in _onCollisionsEnter)
        {
            if (onCollisionEnter.layerMask.Contains(go.layer))
            {
                onCollisionEnter.unityEvent.Invoke(go);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject go = collision.gameObject;

        foreach (CollisionEvent onCollisionStay in _onCollisionsStay)
        {
            if (onCollisionStay.layerMask.Contains(go.layer))
            {
                onCollisionStay.unityEvent.Invoke(go);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject go = collision.gameObject;

        foreach (CollisionEvent onCollisionExit in _onCollisionsExit)
        {
            if (onCollisionExit.layerMask.Contains(go.layer))
            {
                onCollisionExit.unityEvent.Invoke(go);
            }
        }
    }
   
}
