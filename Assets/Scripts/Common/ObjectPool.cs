using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I dont like that i cant use this. Try to look into reflection to make it able to create the objects as well
// If you cant do that, just delete this and do everythinag in gameobjectpool
public abstract class ObjectPool<T>
{
    protected T[] pooledObjects;
    protected T objectToPool;
    protected int currentIndex;
    protected Queue<int> freeObjects = new Queue<int>();

    public ObjectPool(T objectToPool, int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        pooledObjects = new T[poolSize];

        // Make every object free to use
        for (int i = 0; i < poolSize; i++)
            freeObjects.Enqueue(i);
    }

    public T GetPooledObject()
    {
        // currentIndex = (currentIndex + 1) % (pooledObjects.Length - 1);
        // Check if the queue is empty. If it is, return a new object or recycle the oldest one?
        return pooledObjects[freeObjects.Dequeue()];
    }

    public (T, int) GetPooledObjectWithIndex()
    {
        int index = freeObjects.Dequeue();
        return (pooledObjects[index], index);
    }

    public void ReturnPooledObject(int index)
    {
        freeObjects.Enqueue(index);
    }
}

public class GameObjectPool : ObjectPool<GameObject>
{
    public GameObjectPool(GameObject objectToPool, int poolSize = 10) : base(objectToPool, poolSize)
    {
        GameObject poolParent = new GameObject();
        poolParent.name = "GameObjectPool";

        for (int i = 0; i < poolSize; i++)
        {
            pooledObjects[i] = GameObject.Instantiate(objectToPool, poolParent.transform);
            pooledObjects[i].name = "Pooled " + pooledObjects[i].name + " " + i;
            pooledObjects[i].SetActive(false);
        }
    }

}
