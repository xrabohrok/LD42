using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGunScript : BaseGunScript
{
    private LineRenderer laserBeam;

    public override void Start()
    {
        bulletDamage = 50;
        cooldownPeriod = 0.5f;

        laserBeam = GetComponent<LineRenderer>();
        laserBeam.enabled = false;
        laserBeam.useWorldSpace = true;
        
        base.Start();
    }

    public override void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 gunPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - gunPos;
            direction.Normalize();
            Vector2 endPos = (direction * 10) + gunPos;

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(1 << LayerMask.NameToLayer("Character"));
            RaycastHit2D[] results = new RaycastHit2D[10];

            Physics2D.Linecast(gunPos, endPos, filter, results);

            foreach(RaycastHit2D hit in results)
            {
                if(hit.collider && hit.collider.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyTopDownMovement>().TakeDamage(bulletDamage);
                    
                }

            }
            laserBeam.enabled = true;
            laserBeam.SetPosition(0, gunPos);
            laserBeam.SetPosition(1,worldPoint);
            StartCoroutine(Fade());

        }
        else
        {
            laserBeam.enabled = false;
           
        }
    }
    
    IEnumerator Fade() {
        yield return new WaitForSeconds(.5f);
        laserBeam.enabled = false;
    }

    
}
