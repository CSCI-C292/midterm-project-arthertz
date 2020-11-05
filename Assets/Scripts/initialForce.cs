using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialForce : MonoBehaviour
{

    [SerializeField]
    float _force;

    [SerializeField]
    Vector3 _direction;

    [SerializeField] ForceMode _forceMode;

    [SerializeField]
    bool _local = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>() is Rigidbody body)
        {
            if (_local)
            {
                _direction = transform.worldToLocalMatrix * _direction;
            }

            body.AddForce(_direction.normalized*_force, ForceMode.Impulse);
        }
    }



}
