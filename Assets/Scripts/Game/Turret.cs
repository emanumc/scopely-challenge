using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _bulletOrigin;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField, Min(1)] private int _shotsPerSecond;
    [SerializeField] private SphereCollider _attackCollider;
    [SerializeField, Min(0)] private float _attackRadius;

    private float _timeSinceLastShot = 0f;
    private Health _closestEnemy;
    private readonly List<Health> _closeEnemies = new List<Health>();

    private void Update()
    {
        _attackCollider.radius = _attackRadius;

        float fireRate = 1f / _shotsPerSecond;
        if (Time.realtimeSinceStartup - _timeSinceLastShot > fireRate)
        {
            _timeSinceLastShot = Time.realtimeSinceStartup;

            if (_closestEnemy != null && _closestEnemy.Value <= 0)
            {
                _closestEnemy = FindClosestEnemy();
            }

            // shoot
            if (_closestEnemy != null)
            {
                Vector3 direction = _closestEnemy.transform.position - transform.position;
                _bulletSpawner.Shoot(_bulletOrigin.position, direction);
            }
        }
    }

    private Health FindClosestEnemy()
    {
        // find closest enemy
        Health closestEnemy = null;
        float minDistance = float.MaxValue;
        for (int i = _closeEnemies.Count - 1; i >= 0; i--)
        {
            Health health = _closeEnemies[i];
            if (health.Value > 0)
            {
                float distance = Vector3.Distance(transform.position, _closeEnemies[i].transform.position);

                if (distance < minDistance)
                {
                    closestEnemy = _closeEnemies[i];
                    minDistance = distance;
                }
            }
            else
            {
                _closeEnemies.RemoveAt(i);
            }
        }

        return closestEnemy;
    }

    public void OnEnemyEnterAttackRadius(GameObject gameObject)
    {
        Health healthEnemy = gameObject.GetComponent<Health>();
        if (healthEnemy != null)
        {
            _closeEnemies.Add(healthEnemy);
        }

        _closestEnemy = FindClosestEnemy();
    }

    public void OnEnemyExitAttackRadius(GameObject gameObject)
    {
        Health healthEnemy = gameObject.GetComponent<Health>();
        if (healthEnemy != null)
        {
            _closeEnemies.Remove(healthEnemy);
        }

        _closestEnemy = FindClosestEnemy();
    }
}
