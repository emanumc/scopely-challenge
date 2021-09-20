using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField, Min(1)] private int _damage;
    [SerializeField] private BulletSpawner _bulletSpawner;

    public void ApplyDamageToGameObject(GameObject go)
    {
        Health health = go.GetComponent<Health>();
        if (health != null)
        {
            health.ApplyDamage(_damage);
        }
    }

    public void DespawnBullet()
    {
        _bulletSpawner.DespawnBullet(gameObject);
    }
}
