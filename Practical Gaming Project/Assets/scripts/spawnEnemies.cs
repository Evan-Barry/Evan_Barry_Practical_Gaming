using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour {
    
    public GameObject enemy;
    public List<Transform> spawnPoints = new List<Transform>();
    public float spawnTime = 5f;

	// Use this for initialization
	void Start () {
       
        InvokeRepeating("Spawn", 0, spawnTime);    

    }

    void Spawn()
    {
        Debug.Log("Spawn start");

        if(spawnPoints.Count > 0)
        {
            Debug.Log("Spawn IF start");

            int spawnPointIndex = Random.Range(0, spawnPoints.Count);

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            spawnPoints.RemoveAt(spawnPointIndex);
        }
        
    }
	
	// Update is called once per frame
	void Update () {

	}
}
