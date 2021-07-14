using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>(true, points);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
