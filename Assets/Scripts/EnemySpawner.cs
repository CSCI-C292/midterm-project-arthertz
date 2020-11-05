using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Enemy enemy;

    public Weapon weapon;

    public int enemiesToSpawn;

    public float spawnDelay;

    public List<Transform> defaultWaypoints = new List<Transform>();

    public Transform player;

    public RuntimeData runtime;

    void Start ()
    { 

        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnEnemies());
    }


    void SpawnEnemy()
    {
        Enemy spawnedEnemy = Enemy.Instantiate(enemy, transform.position, Quaternion.identity);

        spawnedEnemy.Initialize(player, defaultWaypoints, runtime);

        Weapon spawnedWeapon = Weapon.Instantiate(weapon);

        spawnedEnemy.EquipWeapon(spawnedWeapon);
    }

    IEnumerator SpawnEnemies ()
    {
         while (enemiesToSpawn > 0)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
            enemiesToSpawn--;
        }

        GameObject.Destroy(gameObject);
    }
}
