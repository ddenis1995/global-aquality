using System;
using UnityEngine;
using UnityEngine.Pool;

public class MyPool:MonoBehaviour
{
    public GameObject ObjectPrefab;
    private ObjectPool<GameObject> _myPool;
    private Vector3 _position;
    private void Awake()
    {
        _myPool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPooledItem, true, 100, 1000);
    }

    private GameObject CreatePooledItem()
    {
        GameObject go = Instantiate(ObjectPrefab, _position, Quaternion.identity);
        return go;
    }

    private void OnTakeFromPool(GameObject go)
    {
        go.transform.position = _position;
        go.SetActive(true);
    }

    private void OnReturnedToPool(GameObject go)
    {
        go.SetActive(false);
    }

    private void OnDestroyPooledItem(GameObject go)
    {
        Destroy(go);
    }

    public void SpawnObject(Vector3 pos)
    {
        _position = pos;
        GameObject obj = _myPool.Get();
    }

    public void ReleaseObject(GameObject obj)
    {
        _myPool.Release(obj);
    }
}
