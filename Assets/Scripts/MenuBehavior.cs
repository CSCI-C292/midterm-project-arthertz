using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField]
    private Bool _menuActive;


    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().enabled = _menuActive.member;
    }
}
