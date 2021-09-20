using UnityEngine;

public enum TurretType 
{ 
    Regular, 
    Freezing 
}

public class TurretSpawner : MonoBehaviour
{
    [System.Serializable]
    private class TurretTypeToPool : SerializableDictionary<TurretType, PrototypePool> { }

    [SerializeField] private TurretTypeToPool _turretTypeToPool = new TurretTypeToPool();

    private void Start()
    {
        foreach (var pool in _turretTypeToPool.Values)
        {
            pool.CreatePool();
        }
    }

    public void PlaceTurret(TurretType turretType, Vector3 position)
    {
        PrototypePool pool = _turretTypeToPool[turretType];
        pool.Spawn(position);
    }
}
