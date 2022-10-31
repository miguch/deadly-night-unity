using com.ImmersiveMedia.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    Dictionary<string, Pooler> poolDict;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
        poolDict = new Dictionary<string, Pooler>();
    }

    public void AddPool(Pooler pool)
    {
        if(!poolDict.ContainsKey(pool.Id))
        {
            poolDict.Add(pool.Id, pool);
        }
    }

    public void RemovePool(Pooler pool)
    {
        if (!poolDict.ContainsKey(pool.Id))
        {
            poolDict.Remove(pool.Id);
        }
    }

    public Pooler GetPool(string id)
    {
        if (poolDict.ContainsKey(id))
        {
            return poolDict[id];
        }
        else
        {
            Debug.LogWarning("No such pool exists");
        }
        return null;
    }
}
