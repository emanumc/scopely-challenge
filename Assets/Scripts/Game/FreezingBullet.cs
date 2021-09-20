using UnityEngine;

public class FreezingBullet : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField, Range(0f, 1f)] private float _value;
    [SerializeField, Min(0f)] private int _damage;
    [SerializeField, Min(0f)] private float _duration;

    public void ApplyEffect(GameObject go)
    {
        SlowDownEffect effect = go.GetComponent<SlowDownEffect>();
        if (effect == null)
        {
            effect = go.AddComponent<SlowDownEffect>();
            effect.Value = _value;
            effect.Duration = _duration;
        }

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
