using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy_prefab;
    public GameObject[] enemy;


    public float Spawn_interval, Spawn_value;
    public float interval;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Spawnenemy();
    }

    void Spawnenemy()
    {
        interval += Time.deltaTime;
        if (Spawn_interval <= interval)
        {
            Instantiate(enemy_prefab);
            interval = 0;
        }
    }

}
