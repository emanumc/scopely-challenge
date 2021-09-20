using UnityEngine;

public class FreezingBullet : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField, Range(0f, 1f)] private float _value;
    [SerializeField, Min(0f)] private float _duration;

    public void ApplyEffect(GameObject go)
    {
        SlowDownEffect effect = go.GetComponentInParent<SlowDownEffect>();
        if (effect == null)
        {
            effect = go.AddComponentInParent<SlowDownEffect>();
            effect.Value = _value;
            effect.Duration = _duration;
        }
    }

    public void DespawnBullet()
    {
        _bulletSpawner.DespawnBullet(gameObject);
    }
}
