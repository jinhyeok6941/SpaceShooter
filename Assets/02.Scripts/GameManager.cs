using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public List<Transform> points = new List<Transform>();
    public GameObject monsterPrefab;
    public float createTime = 3.0f;

    private bool isGameOver = false;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            if (isGameOver)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

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
