using UnityEngine;

public class RigidbodyMoveTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _maxSpeed = 2.5f;

    private Vector3 _velocity;

    private void FixedUpdate()
    {
        Vector3 nextPosition = Vector3.SmoothDamp(_rigidBody.position, _target.position, ref _velocity, _smoothTime, _maxSpeed);

        _rigidBody.MovePosition(nextPosition);
    }
}

