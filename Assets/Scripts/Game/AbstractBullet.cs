using UnityEngine;

public abstract class AbstractBullet : MonoBehaviour
{
    [SerializeField, Min(1)] protected int _damage;
    [SerializeField] protected BulletSpawner _bulletSpawner;

    public abstract void ApplyEffect(GameObject go);

    public void DespawnBullet()
    {
        _bulletSpawner.DespawnBullet(gameObject);
    }
}
