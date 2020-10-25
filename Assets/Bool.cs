using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="New Persistent Bool")]
public class Bool : ScriptableObject
{

    public bool member = true;

    public Bool() {}

    public Bool(bool val) { member = val; }

    public bool eval ()
    {
        return member;
    }
}
