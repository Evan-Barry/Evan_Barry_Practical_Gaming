using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour {
    
    public GameObject enemy;
	GameObject enemy1, enemy2, enemy3, enemy4, enemy5, enemy6;
	EnemyAI enemy1Script, enemy2Script, enemy3Script, enemy4Script, enemy5Script, enemy6Script;

	// Use this for initialization
	void Start () {

        //rend = GetComponent<Renderer>();

        //Debug.Log("Plane size - " + rend.bounds.size);

		//These 4 enemies for sight testing
        
        //Instantiate(enemy, new Vector3(23, 0.5f, 23), Quaternion.Euler(new Vector3(0,-90,0)));

        //Instantiate(enemy, new Vector3(23, 0.5f, -23), transform.rotation);
        
        //Instantiate(enemy, new Vector3(-23, 0.5f, 23), Quaternion.Euler(new Vector3(0, 180, 0)));

        //Instantiate(enemy, new Vector3(-23, 0.5f, -23), Quaternion.Euler(new Vector3(0, 90, 0)));

		//These 6 enemies for map test

		enemy1 = Instantiate (enemy, new Vector3 (-7, 0.5f, 14), Quaternion.Euler (new Vector3 (0, 270, 0))) as GameObject;
		enemy1Script = enemy1.GetComponent<EnemyAI> ();
		enemy1Script.patrolDistance = 17f;

		//enemy2 = Instantiate (enemy, new Vector3 (-9, 1, -9), Quaternion.Euler (new Vector3 (0, 270, 0))) as GameObject;
		//enemy2Script = enemy2.GetComponent<EnemyAI> ();
		//enemy2Script.patrolDistance = 27f;

		//enemy3 = Instantiate (enemy, new Vector3 (-36, 1, -19), Quaternion.Euler (new Vector3 (0, 90, 0))) as GameObject;
		//enemy3Script = enemy3.GetComponent<EnemyAI> ();
		//enemy3Script.patrolDistance = 27f;

		//enemy4 = Instantiate (enemy, new Vector3 (8, 1, -3), Quaternion.Euler (new Vector3 (0, 270, 0))) as GameObject;
		//enemy4Script = enemy4.GetComponent<EnemyAI> ();
		//enemy4Script.patrolDistance = 12f;

		//enemy5 = Instantiate (enemy, new Vector3 (33, 1, 18), Quaternion.Euler (new Vector3 (0, 180, 0))) as GameObject;
		//enemy5Script = enemy5.GetComponent<EnemyAI> ();
		//enemy5Script.patrolDistance = 10f;

		//enemy6 = Instantiate (enemy, new Vector3 (33, 1, -9), Quaternion.Euler (new Vector3 (0, 180, 0))) as GameObject;
		//enemy6Script = enemy6.GetComponent<EnemyAI> ();
		//enemy6Script.patrolDistance = 10f;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
