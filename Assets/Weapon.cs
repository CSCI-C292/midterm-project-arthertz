using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon : MonoBehaviour
{

    protected bool canFire = true;

    protected float timeToFire = 0;

    [SerializeField]
    protected float fireRate;


    public void Update () {
        if (timeToFire > 0) {
            timeToFire -= Time.deltaTime;
        } else {
            canFire = true;
        }
    }

    abstract public void Fire (Vector3 target);

    abstract public bool IsGun ();

    public bool ReadyToFire () {
        return canFire;
    }
}
