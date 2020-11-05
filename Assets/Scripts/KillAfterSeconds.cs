using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterSeconds : MonoBehaviour
{
    [SerializeField]
    float delay;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAfter());
    }


    IEnumerator  KillAfter () {
        yield return new WaitForSecondsRealtime (delay);
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
