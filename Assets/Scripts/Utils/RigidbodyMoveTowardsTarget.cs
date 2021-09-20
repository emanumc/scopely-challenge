using UnityEngine;

public class RigidbodyMoveTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _maxSpeed = 2.5f;

    private Vector3 _velocity;

    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

    private void FixedUpdate()
    {
        Vector3 targetPosition = _target != null ? _target.position : _rigidBody.position;
        Vector3 nextPosition = Vector3.SmoothDamp(_rigidBody.position, targetPosition, ref _velocity, _smoothTime, MaxSpeed);

        _rigidBody.MovePosition(nextPosition);
    }
}

