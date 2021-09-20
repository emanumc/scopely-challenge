using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private PrototypePool _bulletPool = new PrototypePool();

    private void Start()
    {
        _bulletPool.CreatePool();
    }

    public GameObject Shoot(Vector3 origin, Vector3 direction)
    {
        Quaternion rot = Quaternion.LookRotation(direction);
        GameObject go = _bulletPool.Spawn(origin, rot);

        MoveDirection moveDirection = go.GetComponent<MoveDirection>();
        moveDirection.Direction = direction;

        return go;
    }

    public void DespawnBullet(GameObject go)
    {
        _bulletPool.Despawn(go);
    }
}
