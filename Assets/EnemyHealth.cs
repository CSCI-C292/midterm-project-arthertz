using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField]
    float startHealth;

    float health;

    bool dead = false;

    [SerializeField]
    Rigidbody body;

    [SerializeField]
    GameObject deathObject;


    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
    }

    void OnHit (float damage) {
        health -= damage;

        if (health <= 0 && !dead) {
            OnDeath();
        }
    }

    void OnDeath () {
        dead = true;


        if (GetComponent<Enemy>().currentWeapon is Gun gun) {
            GameObject drop = gun.dropObject;

            Rigidbody dropBody = GameObject.Instantiate(drop, transform.position, transform.rotation).GetComponent<Rigidbody>();
        }


        Rigidbody deathBody = GameObject.Instantiate(deathObject, transform.position, transform.rotation).GetComponent<Rigidbody>();
        
        deathBody.velocity = body.velocity;

        GameObject.Destroy(gameObject);
    }
}
