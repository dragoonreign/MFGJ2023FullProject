using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> Object1 = new List<GameObject>();
    public GameObject zombiePrefab;
    public int zombieToPool;
    public List<GameObject> Object2 = new List<GameObject>();
    public GameObject zombieEMPPrefab;
    public int zombieEMPToPool;
    public List<GameObject> Object3 = new List<GameObject>();
    public GameObject zombieBossPrefab;
    public int zombieBossToPool;
    public List<GameObject> pipeBulletObjects = new List<GameObject>();
    public GameObject zombieGasPrefab;
    public int zombieGasToPool;
    public List<GameObject> bulletObjects = new List<GameObject>();
    public GameObject bulletPrefab;
    public int bulletToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        DoPoolObject(Object1, zombiePrefab, zombieToPool);
        DoPoolObject(Object2, zombieEMPPrefab, zombieEMPToPool);
        DoPoolObject(Object3, zombieBossPrefab, zombieBossToPool);
        DoPoolObject(pipeBulletObjects, zombieGasPrefab, zombieGasToPool);
        DoPoolObject(bulletObjects, bulletPrefab, bulletToPool);
    }

    public void DoPoolObject(List<GameObject> list, GameObject prefab, int amount)
    {
        // list = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amount; i++)
        {
            tmp = Instantiate(prefab);
            tmp.SetActive(false);
            list.Add(tmp);
        }
    }

    public GameObject GetPooledObject(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            if(!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }
        return null;
    }
}
