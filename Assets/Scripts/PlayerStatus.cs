using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
	
	public int hp = 100;
	
	//Goo Varaibles
	private float startTime;
	private float curTime;

	private float dmgTick = 5.0f;

	private bool inGoo = false;

	public int gooDmg = 0;
	
	//end Goo

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(this.hp <= 0)
		{
			Death();
            
			return;
		}
		
		//Player Standing in Goo
		if (inGoo)
		{
			curTime = Time.time;
			//Debug.Log("Player Standing in Goo for " + (curTime - startTime) + " seconds");

			if (curTime - startTime > dmgTick)
			{
				
				TakeDamage(gooDmg);
				ResetTimer();
			}
		}
	}

	public void TakeDamage(int dmg)
	{
		hp -= dmg;
		Debug.Log("OW! Health is now at: " + hp);
	}
	
	public void Death()
	{
		EventManager.TriggerEvent("PLAYER_DIED");
		Destroy(gameObject);
	}
	
	private void ResetTimer()
	{
		startTime = Time.time;
		curTime = 0.0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			Debug.Log("Player Entered Goo");
			gooDmg = other.GetComponent<GooScript>().dmg;
			inGoo = true;
			ResetTimer();
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			Debug.Log("Player Left Goo");
			inGoo = false;
			gooDmg = 0;
			ResetTimer();
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "GooPile")
		{
			Debug.Log("Player Standing in Goo");
			gooDmg = other.GetComponent<GooScript>().dmg;
			inGoo = true;

		}
	}
	
	
}
