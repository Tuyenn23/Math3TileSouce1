using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}

public class ObjectPooler : Singleton<ObjectPooler>
{
    public List<ObjectPoolItem> itemsToPool;

    public List<GameObject> pooledObjects;

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }


    public GameObject GetPooledObject(string tag)
    {
        for (int i = pooledObjects.Count - 1; i >= 0; i--)
        {
            if (pooledObjects[i] != null && !pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
            if (pooledObjects[i] == null)
            {
                pooledObjects.RemoveAt(i);
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}