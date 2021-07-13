using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
            // 충돌지점의 충돌 정보(좌표, 법선벡터, ...)
            ContactPoint contactPoint = coll.GetContact(0);
            // 좌표
            Vector3 pos = contactPoint.point;
            // 법선벡터를 쿼터니언 타입으로 변환
            Quaternion rot = Quaternion.LookRotation(-contactPoint.normal);

            GameObject spark = Instantiate(sparkEffect, pos, rot);
            Destroy(spark, 0.5f);
        }
    }
}
