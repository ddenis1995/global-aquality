using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts
{
    public class MyPool:MonoBehaviour
    {
        public PoolableItem Item;
        private ObjectPool<PoolableItem> _myPool;
        private Vector3 _position;
        private void Awake()
        {
            _myPool = new ObjectPool<PoolableItem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPooledItem, true, 100, 1000);
        }

        private PoolableItem CreatePooledItem()
        {
            PoolableItem item = Instantiate(Item, _position, Quaternion.identity);
            item.Init(KillItem);
            return item;
        }

        private void OnTakeFromPool(PoolableItem item)
        {
            item.transform.position = _position;
            item.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(PoolableItem item)
        {
            item.gameObject.SetActive(false);
        }

        private void OnDestroyPooledItem(PoolableItem item)
        {
            Destroy(item);
        }

        public void SpawnObject(Vector3 pos)
        {
            _position = pos;
            PoolableItem obj = _myPool.Get();
        }

        public void ReleaseObject(PoolableItem item)
        {
            _myPool.Release(item);
        }

        private void KillItem(PoolableItem item)
        {
            _myPool.Release(item);
        }
    }
}
