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

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        monsterTr = GetComponent<Transform>(); // transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}


/*
    유한상태머신 (Finite State Machine) FSM
    NPC AI

*/