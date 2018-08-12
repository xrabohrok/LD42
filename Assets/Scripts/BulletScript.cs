using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


    public int bulletDamage = 10;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
//	private void OnTriggerEnter2D(Collider2D other)
//	{
//		if (other.tag == "Background")
//		{
//			Destroy(this.gameObject);
//		}
//	}
	
	
}
