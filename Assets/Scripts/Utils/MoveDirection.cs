using UnityEngine;

public class MoveDirection : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    [SerializeField, Min(0f)] private float _maxSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _smoothTime = 0.3f;

    private Vector3 _currentVelocity;

    public Vector3 Direction { get => _direction; set => _direction = Vector3.ClampMagnitude(value, 1f); }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = Mathf.Max(value, 0f); }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = Direction * MaxSpeed;
        Vector3 targetPosition = _rigidbody.position + targetVelocity;
        Vector3 nextPosition = Vector3.SmoothDamp(_rigidbody.position, targetPosition, ref _currentVelocity, _smoothTime, MaxSpeed);

        _rigidbody.MovePosition(nextPosition);
    }
}
