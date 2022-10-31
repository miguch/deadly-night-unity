using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Utility
{
    /// <summary>
    /// A simple object spawner that spawns objects using pooling
    /// </summary>
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] string poolerID;
        private Pooler pooler; // The pooler to pull objects from

        [SerializeField] private float allowableSpawnIntervalInSeconds; // The minimum allowable time between object spawns

        [SerializeField] private UnityEvent<GameObject> onSpawnObject; // An event that can provide the spawned object to a listener

        private float lastSpawnTime; // The last time an object was spawned

        private void Awake()
        {
            lastSpawnTime = Time.time;
        }
        private void Start()
        {
            StartCoroutine(WaitTillAfterStart());
        }

        private IEnumerator WaitTillAfterStart()
        {
            yield return new WaitForEndOfFrame();
            pooler = PoolManager.Instance.GetPool(poolerID);
        }

        /// <summary>
        /// Spawns an object from the pool
        /// </summary>
        public void SpawnObject()
        {
            GameObject poolableObject = pooler.GetPooledObject();

            if (poolableObject != null && Time.time - lastSpawnTime > allowableSpawnIntervalInSeconds)
            {
                lastSpawnTime = Time.time;
                poolableObject.transform.position = transform.position;
                poolableObject.transform.rotation = transform.rotation;
                poolableObject.gameObject.SetActive(true);
                onSpawnObject?.Invoke(poolableObject);
            }
        }
    }
}
