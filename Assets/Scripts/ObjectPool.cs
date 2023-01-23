using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ObjectPool
    {
        private readonly Queue<MonoBehaviour> _pool = new Queue<MonoBehaviour>();
        private readonly MonoBehaviour _original;
        private readonly Transform _parent;
        
        public ObjectPool(MonoBehaviour pooledObject, int length, Transform parent)
        {
            _original = pooledObject;
            _parent = parent;
            for (int i = 0; i < length; i++)
            {
                var toPool = Object.Instantiate(_original, _parent, true);
                _pool.Enqueue(toPool);
                toPool.gameObject.SetActive(false);
            }
        }

        public MonoBehaviour GetFromPool()
        {
            if (_pool.Count == 0)
                return Object.Instantiate(_original, _parent, true);
            
            var toReturn = _pool.Dequeue();
            toReturn.gameObject.SetActive(true);
            return toReturn;
        }

        public void ReturnToPool(MonoBehaviour toReturn)
        {
            toReturn.gameObject.SetActive(false);
            _pool.Enqueue(toReturn);
        }
        
    }
}