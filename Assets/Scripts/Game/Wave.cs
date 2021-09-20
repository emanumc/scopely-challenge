using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Game/Creep Wave")]
public class Wave : ScriptableObject
{
    [System.Serializable]
    public class CreepSpawn
    {
        public CreepType creepType;
        public int numSpawns;
        public float spawnIntervalInSeconds = 1f;
    }

    [System.Serializable]
    public class SpawnPointToCreepSpawn : SerializableDictionary<SpawnPoint, CreepSpawn> { }

    [SerializeField] private SpawnPointToCreepSpawn _spawns = new SpawnPointToCreepSpawn();

    public IDictionary<SpawnPoint, CreepSpawn> Spawns 
    {
        get
        {
            return _spawns;
        }
    }
}
