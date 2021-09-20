using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreepType 
{ 
    Small, 
    Big 
}

public class CreepSpawner : MonoBehaviour
{
    [System.Serializable]
    private class CreepTypeToPool : SerializableDictionary<CreepType, PrototypePool> { }

    [SerializeField] private CreepTypeToPool _creepTypeToPool = new CreepTypeToPool();

    private enum SpawnPoint { Northwest, Northeast, Southwest, Southeast }

    [System.Serializable]
    private class SpawnPointToTransform : SerializableDictionary<SpawnPoint, Transform> { }

    [SerializeField] private SpawnPointToTransform _spawnPointToTransform = new SpawnPointToTransform();

    public List<(CreepType, Health)> Enemies { get; private set; } = new List<(CreepType, Health)>();

    public bool FinishSpawning { get; private set; }

    private void Start()
    {
        FinishSpawning = false;

        foreach (var pool in _creepTypeToPool.Values)
        {
            pool.CreatePool();
        }

        StartCoroutine(CreateEnemies());
        StartCoroutine(ManageEnemies());
    }

    private IEnumerator CreateEnemies()
    {
        SpawnPoint[] spawnPoints = (SpawnPoint[])System.Enum.GetValues(typeof(SpawnPoint));

        foreach (var item in _creepTypeToPool)
        {
            for (int i = 0; i < 10; i++)
            {
                CreepType creepType = item.Key;
                PrototypePool pool = item.Value;

                int index = Random.Range(0, spawnPoints.Length);
                SpawnPoint spawnPoint = spawnPoints[index];
                Transform spawnPointTransform = _spawnPointToTransform[spawnPoint];

                GameObject instance = pool.Spawn(spawnPointTransform.position);
                Health health = instance.GetComponent<Health>();
                Enemies.Add((creepType, health));

                yield return new WaitForSeconds(1f);
            }
        }

        FinishSpawning = true;
    }

    private IEnumerator ManageEnemies()
    {
        while (!FinishSpawning || Enemies.Count > 0)
        {
            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                CreepType creepType = Enemies[i].Item1;
                Health health = Enemies[i].Item2;
                if (health.Value <= 0)
                {
                    Enemies.RemoveAt(i);

                    _creepTypeToPool[creepType].Despawn(health.gameObject);
                }
            }
            yield return null;
        }
    }
}
