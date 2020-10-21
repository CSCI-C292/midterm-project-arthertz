using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnKey : MonoBehaviour
{

    [SerializeField]
    KeyCode _pauseKey;

    [SerializeField]
    RuntimeData _runtime;

    float oldTimeScale;
    float oldFixedDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_pauseKey) && _runtime.isPaused == false || _runtime.gameOverMenu && _runtime.isPaused == false)
        {
            _runtime.isPaused = true;
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
            oldFixedDeltaTime = Time.fixedDeltaTime;
            Time.fixedDeltaTime = 0;
        } else if (Input.GetKeyDown(_pauseKey) && _runtime.isPaused && !_runtime.gameOverMenu)
        {
            _runtime.isPaused = false;
            Time.timeScale = oldTimeScale;
            Time.fixedDeltaTime = oldFixedDeltaTime;
        }
    }
}
