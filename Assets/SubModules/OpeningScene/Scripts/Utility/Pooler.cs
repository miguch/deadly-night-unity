using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Utility
{
    /// <summary>
    /// A simple object pooler
    /// </summary>
    public class Pooler : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] int poolSize; // The size of the pool that should be created
        [SerializeField] private GameObject objectToPool; // The prefab that should be pooled
        private List<GameObject> pool; // The list of objects that makes up the pool

        public string Id { get => id;}

        private void Awake()
        {
            // Create the pool
            pool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectToPool, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        private void Start()
        {
            PoolManager.Instance.AddPool(this);
        }

        /// <summary>
        /// Gets an object from the pool if one is available
        /// </summary>
        /// <returns></returns>
        public GameObject GetPooledObject()
        {
            // Find the first inactive object in the pool and returns it
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }

            Debug.LogWarning("Pool is empty. No object can be returned");
            return null;
        }

        private void OnDestroy()
        {
            foreach(GameObject obj in pool)
            {
                Destroy(obj);
            }
            PoolManager.Instance.RemovePool(this);
        }
    }
}
