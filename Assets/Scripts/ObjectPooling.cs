using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PoolObjectType
{
    Bullet,
    BulletCircle
}

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount =  0;
    public GameObject prefab, container;
    
    [HideInInspector]
    public List<GameObject> pools = new List<GameObject>();
}
public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    public List<PoolInfo> listOfPoolInfo;
    
    private Vector3 defaultPos = new Vector3(-100, -100, -100);
    public static ObjectPooling instance;
    void Awake()
    {
        if (instance == null)  
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < listOfPoolInfo.Count; i++)
        {
            FillPool(listOfPoolInfo[i]);
        }
    }
    void FillPool(PoolInfo poolInfo)
    {
        for (int i = 0; i < poolInfo.amount; i++)
        {
            GameObject obj = Instantiate(poolInfo.prefab, poolInfo.container.transform);
            obj.gameObject.SetActive(false);
            obj.transform.position = defaultPos;
            poolInfo.pools.Add(obj);
        }
    }
    public PoolInfo GetPoolByType(PoolObjectType type)
    {
        for(int i = 0; i < listOfPoolInfo.Count; i++)
        {
            if(listOfPoolInfo[i].type == type)
            {
                return listOfPoolInfo[i];
            }
        }
        return null;
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pools = selected.pools;
        GameObject obj;
        if(pools.Count > 0)
        {
            obj = pools[pools.Count - 1];
            pools.Remove(obj);
        }
        else{
            obj = Instantiate(selected.prefab, selected.container.transform);

        }
        return obj;
    }

    public void CoolObject(GameObject obj, PoolObjectType type)
    {
        
        obj.SetActive(false);
        obj.transform.position = defaultPos;

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pools = selected.pools;
        if(!pools.Contains(obj))
        {
            pools.Add(obj);
        }
    }

}
