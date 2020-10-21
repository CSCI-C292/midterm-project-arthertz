using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    [SerializeField]
    RuntimeData _runtime;


    // Update is called once per frame
    void Update()
    {
        GetComponent<Canvas>().enabled = _runtime.gameOverMenu;
    }

}
