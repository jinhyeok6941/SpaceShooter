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

    void Start()
    {
        // Animator Controller의 Parameter의 해시값을 추출
        hashTrace = Animator.StringToHash("IsTrace");

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
                    break;

                case STATE.ATTACK:
                    break;

                case STATE.DIE:
                    break;
            }

            yield return ws;
        }
    }

}


/*
    유한상태머신 (Finite State Machine) FSM
    NPC AI

*/