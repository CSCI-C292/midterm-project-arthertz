using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New RuntimeData")]

public class RuntimeData : ScriptableObject
{

    private RuntimeData ancestor;

    private Enemy enemyTarget;

    WeaponPickup dropTarget;

    public RuntimeData (RuntimeData cloneTarget) {
        //construct a RuntimeData by copying any persistent data
        //when the game is exited, these are copied back to the ancestor
        ancestor = cloneTarget;
    }


    private void OnApplicationQuit() {
        //when the game is exited, persistent data is copied back to the ancestor

    }

    public Enemy GetEnemyTarget () {
        return enemyTarget;
    }

    public void SetEnemyTarget (Enemy newTarget) {
        enemyTarget = newTarget;
    }

}
