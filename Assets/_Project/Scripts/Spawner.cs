using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float EnemiesPerSecond = 1;
    public bool UseObjectPool = false;
    private MyPool _myPool;
    

    private float LastSpawnTime;

    private void Awake()
    {
        _myPool = GetComponent<MyPool>();
    }

    private void Update()
    {
        float delay = 1f/EnemiesPerSecond;
        if (LastSpawnTime + delay < Time.time)
        {
            int enemiesToSpawnInFrame = Mathf.CeilToInt(Time.deltaTime / delay);
            while (enemiesToSpawnInFrame > 0)
            {
                if (!UseObjectPool)
                {
                    
                }
                else
                {
                    _myPool.SpawnObject();
                    Debug.Log("SpawnedObject");
                }
                
                enemiesToSpawnInFrame--;
            }
            
            LastSpawnTime = Time.time;
        }
    }
}
