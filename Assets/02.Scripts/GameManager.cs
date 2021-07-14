using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public GameObject monsterPrefab;
    public float createTime = 3.0f;

    public bool isGameOver = false;

    void Start()
    {
        GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(true, points);

        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }

    void CreateMonster()
    {
        int idx = Random.Range(1, points.Count);
        GameObject monster = Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
    }

}
