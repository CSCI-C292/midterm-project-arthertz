using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    bool playerInRange = false;
    bool pickedUp = false;

    public GameObject _representedObject;

    private void OnTriggerStay (Collider other) {
        if (!pickedUp && Input.GetKeyDown(KeyCode.E) && other.GetComponent<PlayerShoot>() is PlayerShoot player && player.CanPickupGun()) {
            GameEvents.InvokePickupItem(other.gameObject, GameObject.Instantiate(_representedObject));
            pickedUp = true;
            GameObject.Destroy(gameObject);
        }
    }
}
