#pragma warning disable IDE0051

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
    public AnimationClip[] dies;
}

public class PlayerCtrl : MonoBehaviour
{
    private float h;
    private float v;
    private float r;

    [Range(3.0f, 8.0f)]
    public float moveSpeed = 8.0f;
    public PlayerAnim playerAnim;

    private Animation anim;
    private float turnSpeed = 0.0f;

    private float initHp = 100.0f;
    private float currHp = 100.0f;

    // 델리게이트 (Delegate : 대리자) 
    public delegate void PlayerDieHandler();
    // 이벤트 정의
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        anim = GetComponent<Animation>();
        anim.Play(playerAnim.idle.name);


        yield return new WaitForSeconds(0.2f);
        turnSpeed = 100.0f;
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
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * r);

        if (v >= 0.1f)
        {
            // 전진 애니메이션으로 전환
            anim.CrossFade(playerAnim.runForward.name, 0.3f);
        }
        else if (v <= -0.1f)
        {
            // 후진
            anim.CrossFade(playerAnim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            // 오른쪽 이동
            anim.CrossFade(playerAnim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            // 왼쪽으로 이동
            anim.CrossFade(playerAnim.runLeft.name, 0.3f);
        }
        else
        {
            anim.CrossFade(playerAnim.idle.name, 0.3f);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (currHp > 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        // 이벤트 발생(Event Raised)
        OnPlayerDie();

        GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver = true;

        // GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        // foreach (var monster in monsters)
        // {
        //     //monster.GetComponent<MonsterCtrl>().YouWin();
        //     monster.SendMessage("YouWin", SendMessageOptions.DontRequireReceiver);
        // }
    }
}
