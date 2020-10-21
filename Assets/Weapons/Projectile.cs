using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float damage;

    [SerializeField]
    float power;

    [SerializeField]
    float radius;

    [SerializeField]
    string ownerTag;

    [SerializeField]
    float initVelocity;

    [SerializeField]
    Rigidbody body;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != ownerTag && (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Enemy")) ) {

            if (other.gameObject.GetComponent<EnemyHealth>() is EnemyHealth enemy && ownerTag is "Player")
            {
                enemy.SendMessage("OnHit", damage);
            } else if (other.gameObject.GetComponent<PlayerHealth>() is PlayerHealth player && ownerTag is "Enemy")
            {
                player.SendMessage("OnHit", damage);
            }
        }

        Rigidbody body = other.rigidbody;

         if (body) {
             body.AddExplosionForce(power, other.contacts[0].point, radius);
         }
    }

    public void SetProjectileDirection (Vector3 directionAndDeflection) {

        //This is a nasty hack- the unitDirection passes the positive deflection as its magnitude
        float maxPositiveDeflection = directionAndDeflection.magnitude;
        Vector3 unitDirection = directionAndDeflection.normalized;

        float deflectionAngle = Random.Range(-maxPositiveDeflection, maxPositiveDeflection);

        //Apply deflection
        Vector3 newDirection =  Quaternion.AngleAxis(deflectionAngle, Random.onUnitSphere) * unitDirection;

        body.velocity = newDirection * initVelocity;
    }

    public void SetProjectileOwner (string tag) {
        ownerTag = tag;
    }

    internal bool OwnerIs(string v)
    {
        return ownerTag.Equals(v);
    }

    internal float GetDamage()
    {
        return damage;
    }

    
    internal float GetVelocity()
    {
        return initVelocity;
    }
}
