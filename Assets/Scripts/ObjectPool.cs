using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ObjectPool
    {
        private readonly Queue<GameObject> _pool = new Queue<GameObject>();
        private readonly GameObject _original;

        public ObjectPool(GameObject pooledObject, int length)
        {
            _original = pooledObject;
            for (int i = 0; i < length; i++)
            {
                var toPool = Object.Instantiate(_original);
                _pool.Enqueue(toPool);
                toPool.SetActive(false);
            }
        }

        public GameObject GetFromPool()
        {
            if (_pool.Count == 0)
                return Object.Instantiate(_original);
            
            var toReturn = _pool.Dequeue();
            toReturn.SetActive(true);
            return toReturn;
        }

        public void ReturnToPool(GameObject toReturn)
        {
            _pool.Enqueue(toReturn);
        }
        
    }
}