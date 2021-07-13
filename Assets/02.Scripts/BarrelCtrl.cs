using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    private int hitCount;
    public GameObject expEffect;

    private new MeshRenderer renderer;

    public Texture[] textures;

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();

        /*
            Random.Range(0, 3)  // 0, 1, 2
            Random.Range(0.0f, 3.0f) // 0.0f ~ 3.0f
        */
        renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
    }

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
