using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runLeft;
    public AnimationClip runRight;
}

public class PlayerCtrl : MonoBehaviour
{
    private float h;
    private float v;
    private float r;

    [Range(3.0f, 8.0f)]
    public float moveSpeed = 8.0f;
    public PlayerAnim playerAnim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    // 화면을 랜더링 하는 주기
    /* 정규화벡터(Normalized Vector), 단위벡터(Unit Vector)

        Vector3.forward = Vector3(0, 0, 1)
        Vector3.up      = Vector3(0, 1, 0)
        Vector3.right   = Vector3(1, 0, 0)
    
        Vector3.one     = Vector3(1, 1, 1)
        Vector3.zero    = Vector3(0, 0, 0)
    */

    void Update()
    {
        h = Input.GetAxis("Horizontal");    // -1.0f ~ 0.0f ~ +1.0f
        v = Input.GetAxis("Vertical");      // -1.0f ~ 0.0f ~ +1.0f
        r = Input.GetAxis("Mouse X");
        //v = Input.GetAxisRaw("")  // -1.0f, 0.0f, 1.0f

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed);
        transform.Rotate(Vector3.up * Time.deltaTime * 100.0f * r);
    }




}
