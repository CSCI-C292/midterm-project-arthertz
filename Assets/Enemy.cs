using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class Enemy : MonoBehaviour
{

    [SerializeField]
    bool ignorePlayer = false;
    [SerializeField]
    bool neverPatrol = false;
    [SerializeField]
    public Weapon currentWeapon;
    [SerializeField]
    Transform aimTarget;
    [SerializeField]
    float lookThreshold;
    [SerializeField]
    float aimTime;

    [SerializeField]
    Transform _weaponHook;

    [SerializeField]
    float waypointThreshold;

    [SerializeField]
    List<Transform> waypoints = new List<Transform>();

    int waypointIndex = 0;

    [SerializeField]
    int currentState = 0;

    [SerializeField]
    Transform playerTarget;

    [SerializeField]
    NavMeshAgent navAgent;

    Vector3 nextPoint;

    RuntimeData _runtime;

    [SerializeField]
    Transform fireTarget;


    private void Awake() {


        GameEvents.NewRuntime += NewRuntime;
    }

    void NewRuntime(object sender, EventArgs runtimeObject) {
        _runtime = ((RuntimeObject) runtimeObject).runtime;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Count > 0) {
            nextPoint = waypoints[++waypointIndex % waypoints.Count].position;
            navAgent.SetDestination(nextPoint);
        }
    }


    public void EquipWeapon (Weapon weapon)
    {
        currentWeapon = weapon;
        weapon.ownerObject = gameObject;
        weapon.transform.SetParent(_weaponHook);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }


    // Update is called once per frame
    void Update()
    {
        if (_runtime.pauseMenuActive.member) return;

        switch (currentState) {
            case 0:
                //Patrolling for player
                if (waypoints.Count > 0) {
                    PatrolForPlayer ();
                }
                if (CanSee(playerTarget) && !ignorePlayer) {
                    currentState = 1;
                }
                break;
            case 1:
                //Player is found-- time to shoot at them
                
                LerpAim (playerTarget, aimTime);
                ShootAt(playerTarget);

                if (!CanSee(playerTarget) && !neverPatrol) {
                    currentState = 0;
                }
                break;
            default:
                print("Out of behavior scope!");
                break;
        }
    }

    void LerpAim (Transform target, float aimTime) {
        aimTarget.transform.position = Vector3.Lerp(aimTarget.position, target.position, aimTime*Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((aimTarget.transform.position - transform.position).normalized, Vector3.up);
    }

    void PatrolForPlayer () {
        if (waypoints.Count > 0) {
            if (Vector3.Distance(navAgent.pathEndPosition, transform.position) < waypointThreshold && !navAgent.pathPending) {
            nextPoint = waypoints[(1 + waypointIndex++) % waypoints.Count].position;
            navAgent.SetDestination(nextPoint);
            }
        }
    }

    void ShootAt (Transform target) {
        aimTarget.position = target.position;
        if (currentWeapon.IsGun () && currentWeapon.ReadyToFire()) {
            RaycastHit hit = new RaycastHit();
            Physics.Raycast (transform.position, aimTarget.position, out hit, 999f);
            ((Gun) currentWeapon).Fire (playerTarget.position, true);
        }
    }

    private void OnMouseOver() {
        _runtime.SetEnemyTarget(this);
    }

    private void OnMouseExit() {
        _runtime.SetEnemyTarget (null);
    }

    public Vector3 LookDir () {
        return (aimTarget.position - transform.position).normalized;
    }

    public bool CanSee (Transform target) {
        Vector3 targetDir = (target.position - transform.position).normalized;
        //Use the dot product to make a cone of sight
        float sightVar = Mathf.Abs( Vector3.Dot(LookDir(),targetDir) );
        return sightVar < lookThreshold;
    }
}

