using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PairOfObjects : EventArgs {
    public GameObject A;
    public GameObject B;

    public PairOfObjects (GameObject A, GameObject B) {
        this.A = A;

        this.B = B;
    }

}

public class RuntimeObject : EventArgs {
    public RuntimeData runtime;

    public RuntimeObject (RuntimeData r) {
        runtime = r;
    }
}

public static class GameEvents
{

    public static event EventHandler PickupItem;

    public static event EventHandler GameOver;

    public static event EventHandler EnemyDead;

    public static event EventHandler NewRuntime;

    public static void InvokePickupItem (GameObject taker, GameObject item) {
        PickupItem (null, new PairOfObjects(taker, item));
    }

    public static void InvokeGameOver () {
        GameOver (null, EventArgs.Empty);
    }

    public static void InvokeNewRuntime (RuntimeData runtime) {
        NewRuntime (null, new RuntimeObject (runtime));
    }
}
