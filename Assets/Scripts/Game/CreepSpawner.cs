using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawner : MonoBehaviour
{
    private enum CreepType { Small, Big }

    [System.Serializable]
    private class CreepTypeToPool : SerializableDictionary<CreepType, PrototypePool> { }

    [SerializeField] private CreepTypeToPool _creepTypeToPool = new CreepTypeToPool();

    private enum SpawnPoint { Northwest, Northeast, Southwest, Southeast }

    [System.Serializable]
    private class SpawnPointToTransform : SerializableDictionary<SpawnPoint, Transform> { }

    [SerializeField] private SpawnPointToTransform _spawnPointToTransform = new SpawnPointToTransform();

    private IEnumerator Start()
    {
        SpawnPoint[] spawnPoints = (SpawnPoint[])System.Enum.GetValues(typeof(SpawnPoint));

        foreach (var pool in _creepTypeToPool.Values)
        {
            pool.CreatePool();
        }

        foreach (var pool in _creepTypeToPool.Values)
        {
            for (int i = 0; i < 10; i++)
            {
                int index = Random.Range(0, spawnPoints.Length);
                SpawnPoint spawnPoint = spawnPoints[index];
                Transform spawnPointTransform = _spawnPointToTransform[spawnPoint];

                pool.Spawn(spawnPointTransform.position, Quaternion.identity);

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
