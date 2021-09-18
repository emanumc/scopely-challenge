using UnityEngine;

public class RigidbodyRotateTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxDegreesDelta = 0.5f;

    private void FixedUpdate()
    {
        Vector3 targetDirection = _target.position - _rigidbody.position;
        Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion nextRotation = Quaternion.RotateTowards(_rigidbody.rotation, toRotation, _maxDegreesDelta);

        _rigidbody.MoveRotation(nextRotation);

    }
}
