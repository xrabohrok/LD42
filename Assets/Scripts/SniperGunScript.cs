using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGunScript : BaseGunScript {

    public new int bulletDamage = 30;
    
    public new void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 gunPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - gunPos;
            direction.Normalize();
            Vector2 endPos = (direction * 10) + gunPos;
            RaycastHit2D[] results = new RaycastHit2D[10];

            //int length = Physics2D.Linecast(gunPos, endPos, )
            // public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask = DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity); 
            // ublic static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results); 
        }
    }
}
