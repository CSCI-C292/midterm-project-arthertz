using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowWhenStopped : MonoBehaviour
{
    [SerializeField] float threshold = .05f;
    [SerializeField] float lowTimeScale;
    [SerializeField] float highTimeScale;

    
    [SerializeField] Rigidbody body;

    [SerializeField] bool speedUp;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update () {
        if (body.velocity.magnitude > threshold || speedUp) {
            Time.timeScale = highTimeScale;
            Time.fixedDeltaTime =  .02f * Time.timeScale;
        } else {
            Time.timeScale = lowTimeScale;
            Time.fixedDeltaTime = .02f * Time.timeScale;
        }
    }

IEnumerator SpeedForTime (float time) 
{
    speedUp = true;
    for (float ft = time; ft >= 0; ft -= Time.fixedDeltaTime) 
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);
    }
    speedUp = false;
}

    public void SpeedUp (float speedUpTime) {
        if (!speedUp) {
            StartCoroutine("SpeedForTime", speedUpTime);
        }
    }
}
