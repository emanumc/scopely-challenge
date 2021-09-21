using UnityEngine;

public class Bullet : AbstractBullet
{
    public override void ApplyEffect(GameObject go)
    {
        Health health = go.GetComponent<Health>();
        if (health != null)
        {
            health.ApplyDamage(_damage);
        }
    }
}
