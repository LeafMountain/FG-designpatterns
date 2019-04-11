using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDecalSystem_Prefabs : Singleton<BulletDecalSystem_Prefabs>
{
    // private static BulletDecalSystem_Prefabs instance;
    private GameObjectPool bulletDecalPool;

    public BulletDecalSystem_Prefabs()
    {
        // I'm not sure if this is a good solution but it works for now
        GameObject decalPrefab = Resources.Load<GameObject>("BulletHitDecal");
        if (!decalPrefab)
        {
            Debug.Log("No prefab found at Assets/Resources/BulletHitDecal");
            return;
        }

        bulletDecalPool = new GameObjectPool(decalPrefab);
    }

    public GameObject GetDecal()
    {
        return bulletDecalPool.GetPooledObject();
    }
}
