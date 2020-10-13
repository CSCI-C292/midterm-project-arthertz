using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    float initHealth = 100f;
    
    float health;


    // Start is called before the first frame update
    void Start()
    {
        health = initHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Projectile> () is Projectile projectile) {
            if (projectile.OwnerIs("Enemy")) {
                takeDamage (projectile.GetDamage ());
            }
        }
    }


    void takeDamage (float damage) {
        health -= damage;
    }
}
