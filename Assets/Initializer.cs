using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] RuntimeData _initData;

    [SerializeField] bool _instanceRuntimeData = false;


    private void Start() {
        RuntimeData runtime = _instanceRuntimeData ? new RuntimeData (_initData) : _initData;

        GameEvents.InvokeNewRuntime (runtime);
    }
}
