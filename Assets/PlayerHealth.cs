using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float initHealth = 100f;

    [SerializeField]
    bool takeDamage = true;

    [SerializeField]
    float _health;

    [SerializeField]
    RuntimeData _runtime;


    // Start is called before the first frame update
    void Start()
    {
        _health = initHealth;
    }


    public void OnHit (float damage) {
        _health -= damage;
    }

    void Update ()
    {
        if (!_runtime.isPaused && _health < 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver ()
    {
        _runtime.gameOverMenu = true;

        while (_runtime.gameOverMenu)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
