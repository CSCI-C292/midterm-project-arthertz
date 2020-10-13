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

    void NewRuntime(object sender, EventArgs runtimeObject) {
        _runtime = ((RuntimeObject) runtimeObject).runtime;
    }

    Vector3 CalculateFireTarget () {
        //Calculate first hitable from aimCamera
        RaycastHit firstHit = new RaycastHit();
        defaultTarget = aimCamera.position + aimCamera.forward * 999f;
        if (Physics.Raycast(aimCamera.position, defaultTarget - aimCamera.position, out firstHit, 999f)) {
            Debug.DrawRay(aimCamera.position, defaultTarget - aimCamera.position, Color.green, 1f);
            return firstHit.point;
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
        throwObject.AddForce(throwForce * (CalculateFireTarget() - aimCamera.position).normalized, ForceMode.Impulse);
        GameObject.Destroy(currentWeapon.gameObject);
        currentWeapon = fists;
        throwObject.GetComponent<WeaponPickup>()._representedObject = gun.gameObject;
        }
    }

    void OnPickup (object sender, EventArgs args)  {
        
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
            //Set layer to player gun layer
            item.layer = LayerMask.NameToLayer("PlayerGun");
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {

            slowScript.SpeedUp(releaseSpeedupTime);

            currentWeapon.Fire( CalculateFireTarget() );
        }

        if (Input.GetMouseButtonDown(1)) {
            slowScript.SpeedUp(releaseSpeedupTime);

            //If you're holding a weapon, throw it
            if (!currentWeapon.Equals(fists)) {
                ThrowWeapon();
            }
        }
    }
}
