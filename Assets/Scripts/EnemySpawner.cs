using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    private GameObject player;

	public float mapRadius = 3;
	public int spawnDistance = 3;
    private Vector2 center;
	private Vector2 pos;
	
	

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        EventManager.StartListening("ENEMY_DIED", SpawnNewEnemy);
	    center = new Vector2(0, 0);
	}
	
    public void SpawnNewEnemy()
    {
	    Vector2 playerPos = player.transform.position;
	    pos = RandomCircle(center, mapRadius);
	    while (Vector2.Distance(pos, playerPos) < spawnDistance)
	    {
		    pos = RandomCircle(center, mapRadius);
	    }
       
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
	
	private Vector2 RandomCircle ( Vector2 centerCircle ,   float radius  ){
		float ang = Random.value * 360;
		Vector2 position;
		position.x = centerCircle.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		position.y = centerCircle.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return position;
	}
}