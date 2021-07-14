using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum STATE
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public STATE state = STATE.IDLE;

    private Transform playerTr;
    private Transform monsterTr;

    public bool isDie = false;
    public float attackDist = 2.0f;
    public float traceDist = 10.0f;

    private WaitForSeconds ws;
    private NavMeshAgent nv;
    private Animator anim;

    private int hashTrace;
    private int hashAttack;
    private int hashHit;
    private int hashPlayerDie;

    public float hp = 100.0f;

    void OnEnable()
    {
        // 주인공이 사망했을 때 호출되는 이벤트를 연결
        PlayerCtrl.OnPlayerDie += this.YouWin;
    }

    void OnDisable()
    {
        // 주인공이 사망했을 때 호출되는 이벤트를 연결을 해지
        PlayerCtrl.OnPlayerDie -= this.YouWin;
    }

    void Start()
    {
        // Animator Controller의 Parameter의 해시값을 추출
        hashTrace = Animator.StringToHash("IsTrace");
        hashAttack = Animator.StringToHash("IsAttack");
        hashHit = Animator.StringToHash("Hit");
        hashPlayerDie = Animator.StringToHash("PlayerDie");

        ws = new WaitForSeconds(0.3f);
        nv = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        monsterTr = GetComponent<Transform>(); // transform;

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    // 몬스터의 상태를 측정하는 코루틴
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDist)
            {
                state = STATE.ATTACK;
            }
            else if (distance <= traceDist)
            {
                state = STATE.TRACE;
            }
            else
            {
                state = STATE.IDLE;
            }

            yield return ws;
        }

    }

    // 몬스터의 상태값에 따라 적절한 행동(Behaviour) 처리
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (state)
            {
                case STATE.IDLE:
                    nv.isStopped = true;
                    anim.SetBool(hashTrace, false);
                    break;

                case STATE.TRACE:
                    nv.SetDestination(playerTr.position);
                    nv.isStopped = false;

                    anim.SetBool(hashTrace, true);
                    anim.SetBool(hashAttack, false);
                    break;

                case STATE.ATTACK:
                    nv.isStopped = true;
                    anim.SetBool(hashAttack, true);
                    break;

                case STATE.DIE:
                    break;
            }

            yield return ws;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
        }
    }

    public void Damage(float damage)
    {
        anim.SetTrigger(hashHit);
        hp -= damage;

        if (hp <= 0.0f)
        {
            MonsterDie();
        }
    }

    private int hashDie = Animator.StringToHash("Die");

    void MonsterDie()
    {
        StopAllCoroutines();
        nv.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;

        anim.SetTrigger(hashDie);
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

    public void YouWin()
    {
        StopAllCoroutines();
        nv.isStopped = true;

        anim.SetFloat("DanceSpeed", Random.Range(0.8f, 2.0f));
        anim.SetTrigger(hashPlayerDie);
    }
}


/*
    유한상태머신 (Finite State Machine) FSM
    NPC AI

*/