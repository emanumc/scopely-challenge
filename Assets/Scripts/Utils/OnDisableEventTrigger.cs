using UnityEngine;
using UnityEngine.Events;

public class OnDisableEventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> _unityEvent = new UnityEvent<GameObject>();

    private void OnDisable()
    {
        _unityEvent.Invoke(gameObject);
    }
}
