using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    private List<GameObject> poolItems;

    private Transform objectPoolerParent;
    
    public ObjectPooler()
    {
        poolItems = new List<GameObject>();
        objectPoolerParent = GameObject.Find("ObjectPoolerParent").transform;
    }

    public void StorePoolObject(int num, GameObject prefab)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject item = Object.Instantiate(prefab, objectPoolerParent);
            poolItems.Add(item);
            item.SetActive(false);
        }
    }

    public void RemovePoolObjects()
    {
        for (int i = 0; i < objectPoolerParent.childCount; i++)
        {
            Object.Destroy(objectPoolerParent.GetChild(i).gameObject);
        }

        poolItems = new List<GameObject>();
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

        GameObject item = Object.Instantiate(prefab, objectPoolerParent);
        poolItems.Add(item);
        item.SetActive(false);

        return item;
    }

    public GameObject GetPoolObject(GameObject prefab, int enemyType)
    {
        for (int i = 0; i < poolItems.Count; i++)
        {
            if (!poolItems[i].activeInHierarchy)
            {
                if (enemyType == poolItems[i].GetComponent<HealthManager>().GetEnemyType())
                {
                    return poolItems[i];
                }
            }
        }

        GameObject item = Object.Instantiate(prefab, objectPoolerParent);
        poolItems.Add(item);
        item.SetActive(false);

        return item;
    }
}
