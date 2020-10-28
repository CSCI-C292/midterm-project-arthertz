using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : Weapon
{


    public GameObject dropObject;

    public override void Fire(Vector3 target)
    {
        return;
    }

    public override bool IsGun()
    {
        return false;
    }
}
