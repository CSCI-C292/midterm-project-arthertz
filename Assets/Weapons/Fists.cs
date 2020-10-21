using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fists : Weapon
{
    RuntimeData _runtime;

    [SerializeField] float _range;

    [SerializeField] float _damage;

    [SerializeField] float _force;

    private void Awake() {
        GameEvents.NewRuntime += NewRuntime;
    }

    void NewRuntime(object sender, EventArgs runtimeObject) {
        _runtime = ((RuntimeObject) runtimeObject).runtime;
    }


    public override void Fire (Vector3 target) {

        if ( _runtime.GetEnemyTarget() is Enemy enemy) {

            if (!enemy) return;

            if (Vector3.Distance(enemy.transform.position, transform.position) <= _range && canFire)
            {
                RaycastHit hit = new RaycastHit();
                Physics.Raycast(transform.position, transform.forward, out hit, 999f);

                enemy.BroadcastMessage("OnHit", _damage);
                enemy.GetComponent<Rigidbody>().AddExplosionForce(_force, hit.point, _range);

                canFire = false;
                timeToFire = 1 / fireRate;
            }
        }
    }

    public override bool IsGun () {
        return false;
    }

    public override bool ReadyToFire()
    {
        return canFire && _runtime.isPaused;
    }
}
