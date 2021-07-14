using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        ws = new WaitForSeconds(0.3f);

        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        monsterTr = GetComponent<Transform>(); // transform;

        StartCoroutine(CheckMonsterState());
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
}


/*
    유한상태머신 (Finite State Machine) FSM
    NPC AI

*/