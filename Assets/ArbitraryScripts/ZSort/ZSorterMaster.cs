﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Needs top and bottom edge and resolution set.
// Top bound is what is top of the screen (further away in normal isometric view).
// Transforms for sprites need to be at their feet-ish. 

public class ZSorterMaster : MonoBehaviour
{
    public int maxSortOrders = 10;

    public Transform topBound;
    public Transform bottomBound;

    private List<ZSortSlave> allSlaves;

	// Use this for initialization
	void Start () {
	    if (allSlaves == null)
	    {
	        allSlaves = new List<ZSortSlave>();
	    }

	    if (topBound == null)
	    {
	        topBound = this.transform;
	    }

	    if (bottomBound == null)
	    {
	        bottomBound = this.transform;
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (var slave in allSlaves)
	    {
	        slave.SetZDepth(bottomBound.position.y, topBound.position.y, maxSortOrders);
	    }
	}

    public void pledgeFealty(ZSortSlave zSortSlave)
    {
        allSlaves.Add(zSortSlave);
    }

    public void registerDeath(ZSortSlave zSortSlave)
    {
        allSlaves.Remove(zSortSlave);
    }
}
