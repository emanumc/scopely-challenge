using System.Collections;
using UnityEngine;

public class SlowDownEffect : MonoBehaviour
{
    public float Duration { get; set; }
    public float Value { get; set; } // between 0 and 1

    private RigidbodyMoveTowardsTarget _targetMovement;
    private Health _targetHealth;
    private float _elapsedTime = 0f;
    private float _maxSpeedCopy;

    private void Start()
    {
        _targetMovement = GetComponent<RigidbodyMoveTowardsTarget>();
        _targetHealth = GetComponent<Health>();

        StartCoroutine(Apply());
    }

    private IEnumerator Apply()
    {
        _maxSpeedCopy = _targetMovement.MaxSpeed;
        _targetMovement.MaxSpeed = _maxSpeedCopy * Value;
        while (_elapsedTime < Duration && _targetHealth.Value > 0)
        {
            _elapsedTime = Mathf.MoveTowards(_elapsedTime, Duration, Time.deltaTime);
            yield return null;
        }

        _targetMovement.MaxSpeed = _maxSpeedCopy;
        Destroy(this);
    }
}
