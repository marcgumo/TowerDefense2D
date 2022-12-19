using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    private List<GameObject> poolItems;
    
    public ObjectPooler()
    {
        poolItems = new List<GameObject>();
    }

    public void StorePoolObject(int num, GameObject prefab)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject item = Object.Instantiate(prefab);
            poolItems.Add(item);
            item.SetActive(false);
        }
    }

    public GameObject GetPoolObject(GameObject prefab)
    {
        for (int i = 0; i < poolItems.Count; i++)
        {
            if (!poolItems[i].activeInHierarchy)
            {
                return poolItems[i];
            }
        }

        GameObject item = Object.Instantiate(prefab);
        poolItems.Add(item);
        item.SetActive(false);

        return item;
    }
}
