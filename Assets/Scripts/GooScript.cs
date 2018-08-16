using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooScript : MonoBehaviour
{
	

	public int hp = 100;

	public int dmg = 10;
	
	
	
	// Use this for initialization
	void Start () {
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //this thing is likely a ray, give ourselves to it
            other.gameObject.GetComponent<CleanerRay>().doThingToGoo(this.gameObject);
        }
    }

	
}
