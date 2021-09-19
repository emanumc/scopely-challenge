using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent
{
    public LayerMask layerMask;
    public UnityEvent<GameObject> unityEvent;
}

public class OnCollisionEventTrigger : MonoBehaviour
{
    [SerializeField] private CollisionEvent _onCollisionEnter;
    [SerializeField] private CollisionEvent _onCollisionStay;
    [SerializeField] private CollisionEvent _onCollisionExit;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;

        if (_onCollisionEnter.layerMask.Contains(go.layer))
        {
            _onCollisionEnter.unityEvent.Invoke(go);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject go = collision.gameObject;

        if (_onCollisionStay.layerMask.Contains(go.layer))
        {
            _onCollisionStay.unityEvent.Invoke(go);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject go = collision.gameObject;

        if (_onCollisionExit.layerMask.Contains(go.layer))
        {
            _onCollisionExit.unityEvent.Invoke(go);
        }
    }
   
}
