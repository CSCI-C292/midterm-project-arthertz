using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    protected int shotsLeft;


    [SerializeField]
    int ammoInClip;

    [SerializeField]
    float maxBulletDeflection;

    [SerializeField]
    GameObject projectileObject;
    
    [SerializeField]
    Transform bulletSpawn;

    [SerializeField]
    Transform muzzleEnd; 

    public GameObject dropObject;

    public void Start () {
        shotsLeft = ammoInClip;
    }


    public override void Fire (Vector3 target) {
        Fire(target, false);
    }

    public void Fire (Vector3 target, Boolean isEnemy)
    {
        if (canFire && ReadyToFire())
        {
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(muzzleEnd.position, target - muzzleEnd.position, out hit, 999f);
            GameObject g = GameObject.Instantiate(projectileObject, bulletSpawn.position, Quaternion.LookRotation(hit.point - bulletSpawn.position));

            Debug.DrawLine(bulletSpawn.position, hit.point, Color.blue, 10f);

            g.BroadcastMessage("SetProjectileOwner", ownerObject.tag);
            g.BroadcastMessage("SetProjectileDirection", Vector3.Normalize(target - muzzleEnd.position) * maxBulletDeflection);

            if (!isEnemy) shotsLeft--;
            canFire = false;
            timeToFire = 1 / fireRate;
        }
    }
    public override bool IsGun () {
        return true;
    }


    public override bool ReadyToFire()
    {
        return base.ReadyToFire() && shotsLeft > 0;
    }
}
