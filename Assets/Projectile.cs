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

    [SerializeField]
    float maxPositiveDeflection;


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
            
            other.gameObject.BroadcastMessage("OnHit", damage);
        }

        Rigidbody body = other.rigidbody;

         if (body) {
             body.AddExplosionForce(power, other.contacts[0].point, radius);
         }
    }

    public void SetProjectileDirection (Vector3 unitDirection) {

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
