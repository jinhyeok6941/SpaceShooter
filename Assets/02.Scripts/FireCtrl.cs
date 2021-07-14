using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePos;
    public AudioClip fireSfx;

    [SerializeField]
    private MeshRenderer muzzleFlash;

    private new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();

        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx, 0.8f);
        StartCoroutine(this.ShowMuzzleFlash());
        //StartCoroutine("ShowMuzzleFlash");
    }

    // 코루틴 (Co-Routine)
    IEnumerator ShowMuzzleFlash()
    {
        // Texture Offset
        // x,y = 0.0f, 0.5f / (0, 1) * 0.5f => (0.0f, 0.5f)
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        // Rotate 
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);//Quaternion.Euler(0, 0, angle)

        // Scale
        float scale = Random.Range(1.0f, 3.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale; // new Vector3(scale, 1, scale)

        // MuzzleFlash의 MeshRenderer 활성화
        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.3f);

        muzzleFlash.enabled = false;
    }
}


/*
    하늘표현 방식
    1. Skybox (6-Sided Sky)
    2. SkyDome
    3. Procedural Sky

    MipMap


*/