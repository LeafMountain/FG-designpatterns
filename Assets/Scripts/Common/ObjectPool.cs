using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    protected GameObject[] pooledObjects;
    protected GameObject objectToPool;
    protected int currentIndex;
    protected Queue<int> freeObjects = new Queue<int>();
    protected Queue<int> occupiedObjects = new Queue<int>();

    public GameObjectPool(GameObject objectToPool, int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        pooledObjects = new GameObject[poolSize];

        // Just to keep it organized
        GameObject poolParent = new GameObject();
        poolParent.name = "GameObjectPool";

        for (int i = 0; i < poolSize; i++)
        {
            pooledObjects[i] = GameObject.Instantiate(objectToPool, poolParent.transform);
            pooledObjects[i].name = "Pooled " + pooledObjects[i].name + " " + i;
            pooledObjects[i].SetActive(false);
            freeObjects.Enqueue(i);
        }
    }

    public GameObject GetPooledObject()
    {
        return pooledObjects[GetNextIndex()];
    }

    public (GameObject, int) GetPooledObjectWithIndex()
    {
        int index = GetNextIndex();
        return (pooledObjects[index], index);
    }

    public void ReturnPooledObject(int index)
    {
        freeObjects.Enqueue(index);
    }

    public int GetNextIndex()
    {
        if (freeObjects.Count == 0)
            freeObjects.Enqueue(occupiedObjects.Dequeue());

        // Get index of free object
        int index = freeObjects.Dequeue();
        // Add index to occupied objects
        occupiedObjects.Enqueue(index);
        return index;
    }
}
