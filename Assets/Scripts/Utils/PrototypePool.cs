using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrototypePool
{
    [SerializeField] private GameObject _prototype;
    [SerializeField] private Transform _parent;
    [SerializeField, Min(0)] private int _initialSize;

    private readonly Stack<GameObject> _pooledInstances = new Stack<GameObject>();

    public void CreatePool()
    {
        while (_pooledInstances.Count < _initialSize)
        {
            GameObject pooledInstance = Object.Instantiate(_prototype);
            pooledInstance.transform.SetParent(_parent);
            pooledInstance.SetActive(false);
            _pooledInstances.Push(pooledInstance);
        }
    }

    public void DestroyPool()
    {
        var e = _pooledInstances.GetEnumerator();
        while (e.MoveNext())
        {
            Object.Destroy(e.Current);
        }

        _pooledInstances.Clear();
    }

    public GameObject Spawn()
    {
        return Spawn(Vector3.zero, Quaternion.identity);
    }

    public GameObject Spawn(Vector3 position)
    {
        return Spawn(position, Quaternion.identity);
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        if (_pooledInstances.Count > 0)
        {
            GameObject pooledInstance = _pooledInstances.Pop();
            pooledInstance.SetActive(true);
            pooledInstance.transform.position = position;
            pooledInstance.transform.rotation = rotation;
            return pooledInstance;
        }
        else
        {
            GameObject pooledInstance = Object.Instantiate(_prototype);
            pooledInstance.SetActive(true);
            pooledInstance.transform.position = position;
            pooledInstance.transform.rotation = rotation;
            return pooledInstance;
        }
    }

    public void Despawn(GameObject go)
    {
        go.transform.SetParent(_parent);
        go.SetActive(false);
        _pooledInstances.Push(go);
    }
}
