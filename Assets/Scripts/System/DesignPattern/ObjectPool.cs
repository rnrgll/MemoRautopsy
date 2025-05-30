using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

namespace DesignPattern
{
    public class ObjectPool
    {
        private PooledObject prefab;
        private Transform parent;
        private Stack<PooledObject> pool = new Stack<PooledObject>();

        public ObjectPool(PooledObject prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
        }

        public void Init(int num)
        {
            for (int i = 0; i < num; i++)
            {
                PooledObject instance = Instantiate(prefab, parent);
                instance.gameObject.SetActive(false);
                instance.SetPool(this);
                pool.Push(instance);
            }
        }

        public PooledObject Get()
        {
            PooledObject instance;
            if (pool.Count <= 0)
            {
                instance = Instantiate(prefab, parent);
                instance.SetPool(this);
            }
            else
            {
                instance = pool.Pop();
            }

            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Release(PooledObject poolObject)
        {
            poolObject.gameObject.SetActive(false);
            pool.Push(poolObject);
        }

        public void Clear()
        {
            foreach (var obj in pool)
            {
                Destroy(obj.gameObject);
            }
            pool.Clear();
        }
    }
}