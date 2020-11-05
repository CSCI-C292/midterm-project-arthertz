using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [SerializeField]
    float checkInterval = 1f;

    GameObject[] livingEnemies;

    GameObject[] activeSpawners;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckEnemiesDeadRecurring");
    }

    IEnumerator CheckEnemiesDeadRecurring () {
        livingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        activeSpawners  = GameObject.FindGameObjectsWithTag("Spawner");

        while (livingEnemies.Length + activeSpawners.Length > 0) {
            livingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            activeSpawners  = GameObject.FindGameObjectsWithTag("Spawner");
            print(livingEnemies.Length + " " + activeSpawners.Length);
            yield return new WaitForSecondsRealtime(checkInterval);
        }
        print ("Loading scene..." + ( SceneManager.GetActiveScene().buildIndex + 1));
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
