using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnKey : MonoBehaviour
{

    [SerializeField]
    KeyCode _pauseKey;

    [SerializeField]
    Bool isPaused;

    [SerializeField]
    Bool gameOverActive;

    float oldTimeScale;
    float oldFixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_pauseKey) && isPaused.member == false || gameOverActive.member && isPaused.member == false)
        {
            if (!gameOverActive.member) isPaused.member = true;
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
            oldFixedDeltaTime = Time.fixedDeltaTime;
            Time.fixedDeltaTime = 0;
        } else if (Input.GetKeyDown(_pauseKey) && isPaused.member && !gameOverActive.member)
        {
            isPaused.member = false;
            Time.timeScale = oldTimeScale;
            Time.fixedDeltaTime = oldFixedDeltaTime;
        }
    }
}
