using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{

    protected int shotsLeft;


    [SerializeField]
    int ammoInClip;

    [SerializeField]
    GameObject projectileObject;
    
    [SerializeField]
    Transform bulletSpawn;

    [SerializeField]
    Transform muzzleEnd;

    [SerializeField]
    public GameObject ownerObject;

    [SerializeField]
    public GameObject dropObject;

    public void Start () {
        shotsLeft = ammoInClip;
    }


    public override void Fire (Vector3 target) {
        if (shotsLeft > 0 && canFire) {
            RaycastHit hit = new RaycastHit();
            Physics.Raycast (muzzleEnd.position, target - muzzleEnd.position, out hit, 999f);
            GameObject g = GameObject.Instantiate(projectileObject, bulletSpawn.position, Quaternion.LookRotation(target- muzzleEnd.position, muzzleEnd.up));
            
            g.BroadcastMessage("SetProjectileOwner", ownerObject.tag);
            g.BroadcastMessage("SetProjectileDirection", Vector3.Normalize(target - muzzleEnd.position));
        
            shotsLeft--;
            canFire = false;
            timeToFire = 1/fireRate;
        }
    }

    public override bool IsGun () {
        return true;
    }

}
