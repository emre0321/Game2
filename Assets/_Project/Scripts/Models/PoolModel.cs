using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolModel : ObjectModel
{
    [SerializeField] int PoolSize;
    [SerializeField] GameObject PoolElementPrefab;


    [SerializeField] List<ObjectModel> PoolElements;

    public override void Initialize()
    {
        GeneratePool();
    }


    public void GeneratePool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject createdPoolElement = Instantiate(PoolElementPrefab);
            createdPoolElement.transform.SetParent(this.transform);
            PoolElements.Add(createdPoolElement.GetComponent<ObjectModel>());
            createdPoolElement.gameObject.SetActive(false);
        }
    }


    public virtual T GetDeactiveItem<T>()
    {
        for (int i = 0; i < PoolElements.Count; i++)
        {
            if (PoolElements[i].gameObject.activeInHierarchy == false)
            {
                return (T)((object)PoolElements[i].GetComponent<T>());
            }
        }

        return default(T);
    }

}
