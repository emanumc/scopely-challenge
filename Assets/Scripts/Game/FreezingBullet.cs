using UnityEngine;

public class FreezingBullet : AbstractBullet
{
    [SerializeField, Range(0f, 1f)] private float _value;
    [SerializeField, Min(0f)] private float _duration;

    public override void ApplyEffect(GameObject go)
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
}
