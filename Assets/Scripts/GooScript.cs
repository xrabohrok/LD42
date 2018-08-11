using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooScript : MonoBehaviour
{
	
	public GameObject player;

	public int hp = 100;

	public int dmg = 10;
	
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
	}

	public void TakeDamage (int amount)
    {
        hp -= amount;
    }

	
}
