using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemy;

    public Weapon weapon;

    public int enemiesToSpawn;

    public float spawnDelay;


    void Start ()
    {
        StartCoroutine(SpawnEnemies());
    }


    void SpawnEnemy()
    {
        Enemy spawnedEnemy = Enemy.Instantiate(enemy, transform.position, Quaternion.identity);
        Weapon spawnedWeapon = Weapon.Instantiate(weapon);
        spawnedEnemy.EquipWeapon(spawnedWeapon);
    }

    IEnumerator SpawnEnemies ()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }
}
