using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    private ObjectPool<GameObject> bulletPool;

    float shootTime;
    float shootInterval = 0.1f;

    [System.NonSerialized]
    public Player owner;

    void Start()
    {
        bulletPool = new GameObjectPool(Bullet, 100);
    }

    public void Shoot()
    {
        if (shootTime <= Time.time)
        {
            if (!Bullet)
                return;

            (GameObject, int) pooledBullet = bulletPool.GetPooledObjectWithIndex();
            Bullet spawnedBullet = pooledBullet.Item1.GetComponent<Bullet>();
            spawnedBullet.transform.position = transform.position;
            spawnedBullet.transform.rotation = transform.rotation;
            spawnedBullet.owner = owner;
            spawnedBullet.poolIndex = pooledBullet.Item2;
            spawnedBullet.poolOwner = this;
            shootTime = Time.time + shootInterval;
            spawnedBullet.Launch();
        }
    }

    public void ReturnBullet(int index)
    {
        bulletPool.ReturnPooledObject(index);
    }

    #region 
    // public GameObject bullet;

    // float shootTime;
    // float shootInterval = 0.1f;

    // [System.NonSerialized]
    // public Player owner;

    // //
    // // No pool
    // //

    // public void Shoot() {
    // 	if(shootTime <= Time.time) {
    // 		var b = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
    // 		b.owner = owner;
    // 		shootTime = Time.time + shootInterval;
    // 	}
    // }





    //
    // Pooled 1 (more "pure")
    //
    /*
	Bullet[] bulletPool = new Bullet[10];
	int bulletPoolFreeIndex = 0;

	void Start() {
		for(int i = 0; i < bulletPool.Length; i++) {
			bulletPool[i] = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
			bulletPool[i].name = "Bullet (pooled) " + i.ToString();
			bulletPool[i].owner = owner;
			bulletPool[i].poolOwner = this;
			bulletPool[i].gameObject.SetActive(false);
		}
	}
	
	public void Shoot() {
		if(shootTime <= Time.time && bulletPoolFreeIndex < bulletPool.Length) {
			bulletPool[bulletPoolFreeIndex].transform.position = transform.position;
			bulletPool[bulletPoolFreeIndex].transform.rotation = transform.rotation;
			bulletPool[bulletPoolFreeIndex].poolIndex = bulletPoolFreeIndex;
			bulletPool[bulletPoolFreeIndex].gameObject.SetActive(true);
			bulletPool[bulletPoolFreeIndex].Launch();
			bulletPoolFreeIndex++;
			shootTime = Time.time + shootInterval;
		}
	}

	public void ReturnBullet(int index) {
		bulletPoolFreeIndex--;
		var returningBullet = bulletPool[index];
		bulletPool[index] = bulletPool[bulletPoolFreeIndex]; // swap places with the last bullet currently in use
		bulletPool[index].poolIndex = index;
		bulletPool[bulletPoolFreeIndex] = returningBullet;
		returningBullet.gameObject.SetActive(false);
	}
	*/




    //
    // Pooled 2
    //
    /*
	Bullet[] bulletPool = new Bullet[10];
	int bulletPoolFreeIndex = 0;

	void Start() {
		for(int i = 0; i < bulletPool.Length; i++) {
			bulletPool[i] = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Bullet>();
			bulletPool[i].name = "Bullet (pooled) " + i.ToString();
			bulletPool[i].owner = owner;
			bulletPool[i].poolOwner = this;
			bulletPool[i].gameObject.SetActive(false);
		}
	}
	
	public void Shoot() {
		if(shootTime <= Time.time) {
			bulletPool[bulletPoolFreeIndex].transform.position = transform.position;
			bulletPool[bulletPoolFreeIndex].transform.rotation = transform.rotation;
			bulletPool[bulletPoolFreeIndex].poolIndex = bulletPoolFreeIndex;
			bulletPool[bulletPoolFreeIndex].gameObject.SetActive(true);
			bulletPool[bulletPoolFreeIndex].Launch();
			bulletPoolFreeIndex++;
			if(bulletPoolFreeIndex >= bulletPool.Length)
				bulletPoolFreeIndex = 0;
			shootTime = Time.time + shootInterval;
		}
	}
	*/
    #endregion
}
