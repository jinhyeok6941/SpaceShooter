using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public GameObject expEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        GameObject exp = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(exp, 6.0f);

        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 1200.0f);
        Destroy(this.gameObject, 2.0f);
    }
}
