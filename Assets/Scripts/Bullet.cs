using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    float speed = 22f;

    public Player owner;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.Kill(owner);
        }

        ReturnToPool();
        SpawnDecal(collision.contacts[0]);
    }

    #region My Solution

    [System.NonSerialized]
    public Gun poolOwner;

    [System.NonSerialized]
    public int poolIndex;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch()
    {
        gameObject.SetActive(true);
        rb.velocity = transform.forward * speed;
        StopAllCoroutines();
        StartCoroutine(ReturnToPoolAfterAWhile());
    }

    IEnumerator ReturnToPoolAfterAWhile()
    {
        yield return new WaitForSeconds(3f);
        ReturnToPool();
    }

    void ReturnToPool()
    {
        // Should tell the pool that this object is available
        gameObject.SetActive(false);
        poolOwner.ReturnBullet(poolIndex);
    }

    void SpawnDecal(ContactPoint contactPoint)
    {
        var decalSystem = BulletDecalSystem_Prefabs.GetInstance();
        GameObject decal = decalSystem.GetDecal();
        decal.transform.position = contactPoint.point;
        decal.transform.forward = -contactPoint.normal;
        decal.SetActive(true);
    }

    #endregion

    #region No Pool
    //
    // No pool
    //

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    //     rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);

    //     Destroy(gameObject, 3f);
    // }
    #endregion

    #region Pooled 1
    //
    // Pooled 1
    //
    /*
		private void Awake() {
			rb = GetComponent<Rigidbody>();
		}

		public void Launch() {
			rb.velocity = transform.forward * speed;
			StopAllCoroutines();
			StartCoroutine(ReturnToPoolAfterAWhile());
		}

		IEnumerator ReturnToPoolAfterAWhile() {
			yield return new WaitForSeconds(3f);
			poolOwner.ReturnBullet(poolIndex);
		}

		[System.NonSerialized]
		public Gun poolOwner;
		[System.NonSerialized]
		public int poolIndex;
		*/
    #endregion

    #region Pooled 2
    //
    // Pooled 2
    //
    /*
private void Awake() {
    rb = GetComponent<Rigidbody>();
}

public void Launch() {
    rb.velocity = transform.forward * speed;
    StopAllCoroutines();
    StartCoroutine(DisableAfterAWhile());
}

IEnumerator DisableAfterAWhile() {
    yield return new WaitForSeconds(3f);
    gameObject.SetActive(false);
}

[System.NonSerialized]
public Gun poolOwner;
[System.NonSerialized]
public int poolIndex;
*/

    #endregion
}
