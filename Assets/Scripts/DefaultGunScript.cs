using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGunScript : BaseGunScript {

    public override void Start()
    {
        cooldownPeriod = 0.1f;
        bulletDamage = 20;
        base.Start();
    }
}
