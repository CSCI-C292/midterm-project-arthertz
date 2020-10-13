using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    public bool active = false;

    [SerializeField]
    Rigidbody body;

    [SerializeField]
    GameObject spawnOnCollide;

    public int bouncesLeft = 1;

    public bool probabilisticBounce = false;

    public float bounceChance = .3f;

    public float velocityThresh = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (body && velocityThresh > 0) {
            if (body.velocity.sqrMagnitude < velocityThresh) {
                 GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {

        bool willBounce = false;

        if (probabilisticBounce) {
            if (Random.Range(0f, 1f) <= bounceChance) {
                willBounce = true;
            }
        } else {
            if (bouncesLeft-- > 0) {
                willBounce = true;
            }
        }


        if (willBounce && !other.gameObject.CompareTag("IgnoreBullet")) {
            GameObject.Instantiate(spawnOnCollide, transform.position, transform.rotation);
            bouncesLeft --;
        } else {
            if (active && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("IgnoreBullet")) {
            GameObject.Destroy(gameObject);
            GameObject.Instantiate(spawnOnCollide, transform.position, transform.rotation);
            }
        }
    }
}
