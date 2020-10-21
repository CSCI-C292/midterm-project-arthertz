using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowWhenStopped : MonoBehaviour
{
    [SerializeField] float threshold = .05f;
    [SerializeField] float lowTimeScale;
    [SerializeField] float highTimeScale;
    [SerializeField] bool zoomFOVOnSlow = false;
    [SerializeField] Camera cam;
    [SerializeField] float zoomTime = 0;
    [SerializeField] float timeRealFOV;
    [SerializeField] float timeSlowedFOV;
    [SerializeField] RuntimeData _runtime;

    
    [SerializeField] Rigidbody body;

    [SerializeField] bool speedUp;


    // Update is called once per frame
    void Update () {

        if (!_runtime.isPaused)
        {
            if (body.velocity.magnitude > threshold || speedUp)
            {

                Time.timeScale = highTimeScale;
                Time.fixedDeltaTime = .02f * Time.timeScale;
            }
            else
            {
                Time.timeScale = lowTimeScale;
                Time.fixedDeltaTime = .02f * Time.timeScale;
            }
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


IEnumerator PullCameraBack ()
    {
        for (float ft = 0; ft < zoomTime; ft += Time.fixedDeltaTime)
        {
            cam.fieldOfView = Mathf.Lerp(timeSlowedFOV, timeRealFOV, ft / zoomTime);
            yield return new WaitForEndOfFrame();
        }
    }

IEnumerator PullCameraIn()
    {
        for (float ft = 0; ft < zoomTime; ft += Time.fixedDeltaTime)
        {
            cam.fieldOfView = Mathf.Lerp(timeRealFOV, timeSlowedFOV, ft / zoomTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
