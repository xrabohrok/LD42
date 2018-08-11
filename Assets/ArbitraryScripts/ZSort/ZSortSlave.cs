using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSortSlave : MonoBehaviour
{
    public float height = 0;

    private SpriteRenderer render;

    private static ZSorterMaster master;

	// Use this for initialization
	void Start () {

	    if (master == null)
	    {
	        master = GameObject.FindObjectOfType<ZSorterMaster>();
	    }

	    master.pledgeFealty(this);

	    render = this.GetComponent<SpriteRenderer>();

	}

    public void SetZDepth(float minY, float maxY, int maxSortOrder)
    {
        if (render != null)
        {
            render.sortingOrder = Mathf.FloorToInt(( 1 -(( this.transform.position.y + height - minY) / ( maxY - minY))) * maxSortOrder);
        }
    }

    public void OnDestroy()
    {
        master.registerDeath(this);
    }
}
