using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.StartListening("ENEMY_DIED", SpawnNewEnemy);
	}
	
    public void SpawnNewEnemy()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 position = playerPos;
        while(Vector2.Distance(position, playerPos) < 4)
        {
            // Once the map is drawn and there's a collider for the outside of it, try and use that instead. -mw
            // Also maybe an escape clause just so this doesn't accidentally turn infinite. -mw
            position = Random.insideUnitCircle * 4;
        }
        Instantiate(enemyPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
    }
}