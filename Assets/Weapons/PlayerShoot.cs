using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] Weapon currentWeapon;

    [SerializeField] Weapon fists;

    [SerializeField] slowWhenStopped slowScript;

    [SerializeField] Transform aimCamera;

    [SerializeField] Transform throwHook;

    
    [SerializeField] Transform weaponHook;

    [SerializeField] float releaseSpeedupTime;

    [SerializeField] float throwForce;

    RuntimeData _runtime;


    Vector3 defaultTarget;


    private void Awake() {
        GameEvents.NewRuntime += NewRuntime;
        GameEvents.PickupItem += OnPickup;
    }

    private void OnDestroy()
    {
        GameEvents.NewRuntime -= NewRuntime;
        GameEvents.PickupItem -= OnPickup;
    }

    void NewRuntime(object sender, EventArgs runtimeObject) {
        _runtime = ((RuntimeObject) runtimeObject).runtime;
    }

    Vector3 CalculateFireTarget () {
        //Calculate first hitable from aimCamera
        RaycastHit firstHit = new RaycastHit();
        defaultTarget = aimCamera.position + aimCamera.forward * 999f;
        if (Physics.Raycast(aimCamera.position, defaultTarget - aimCamera.position, out firstHit, 999f)) {
            Debug.DrawRay(aimCamera.position, defaultTarget - aimCamera.position, Color.green, 1f);
            //return firstHit.point;

            return defaultTarget;
        } else {
            return defaultTarget;
        }
    }

    public bool CanPickupGun () {
        return currentWeapon.Equals(fists);
    }

    void ThrowWeapon () {
        if (currentWeapon is Gun gun) {
        Rigidbody throwObject = GameObject.Instantiate(gun.dropObject, throwHook.position, throwHook.rotation).GetComponent<Rigidbody>();
        throwObject.GetComponent<Projectile>().SetProjectileOwner(tag);
        throwObject.AddForce(throwForce * (CalculateFireTarget() - aimCamera.position).normalized, ForceMode.Impulse);
        GameObject.Destroy(currentWeapon.gameObject);
        currentWeapon = fists;

            if (throwObject.GetComponent<WeaponPickup>() is WeaponPickup drop)
            {
                if (!drop.representedObject)
                {
                    print("ERROR! Player drop has no pickup option");
                }
            } else
            {
                print("Player dropped something that can't be picked up...");
            }
        }
    }

    void OnPickup (object sender, EventArgs args)  {
        if (gameObject == null) return;

        GameObject taker = ((PairOfObjects) args).A;
        GameObject item = ((PairOfObjects) args).B;

        if (taker.Equals(gameObject)) {
            currentWeapon = item.GetComponent<Weapon>();
            item.transform.SetParent( weaponHook );
            item.transform.position = weaponHook.transform.position;
            item.transform.rotation = weaponHook.transform.rotation;
            if (currentWeapon is Gun gun) {
                gun.ownerObject = gameObject;
            }
            
            //Set layer to player gun layer for each child of the gun
            foreach (Transform t in item.GetComponentsInChildren<Transform>(true))
            {
                t.gameObject.layer = LayerMask.NameToLayer("PlayerGun");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !_runtime.isPaused) {

            slowScript.SpeedUp(releaseSpeedupTime);

            currentWeapon.Fire(CalculateFireTarget());
        }

        if (Input.GetMouseButtonDown(1) && !_runtime.isPaused) {
            slowScript.SpeedUp(releaseSpeedupTime);

            //If you're holding a weapon, throw it
            if (!currentWeapon.Equals(fists)) {
                ThrowWeapon();
            }
        }
    }
}
