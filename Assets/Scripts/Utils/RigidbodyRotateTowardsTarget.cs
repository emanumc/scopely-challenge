using UnityEngine;

public class RigidbodyRotateTowardsTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxDegreesDelta = 0.5f;

    public Transform Target { get => _target; set => _target = value; }

    private void FixedUpdate()
    {
        Vector3 targetDirection = Target != null ? Target.position - _rigidbody.position : transform.forward;

        if (targetDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion nextRotation = Quaternion.RotateTowards(_rigidbody.rotation, toRotation, _maxDegreesDelta);

            _rigidbody.MoveRotation(nextRotation);
        }
    }
}
