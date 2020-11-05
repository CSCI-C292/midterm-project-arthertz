using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSomeLevel : MonoBehaviour
{
    [SerializeField] string name;


    public void Run () {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
