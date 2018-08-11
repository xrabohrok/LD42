using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunScript : MonoBehaviour {

    public GameObject player;
    public GameObject bulletPrefab;
    public int bulletDamage;
    public float bulletSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = player.transform.position;
	}

    public virtual void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 gunPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - gunPos;
            direction.Normalize();
            // Spawn bullet object from player position moving in vector between player and mouse cursor.
            GameObject shotFired = Instantiate(bulletPrefab, new Vector3(gunPos.x, gunPos.y, 0), Quaternion.identity);
            shotFired.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }
}
