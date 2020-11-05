using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline3D))]
public class WeaponPickup : MonoBehaviour
{
    bool playerInRange = false;
    bool pickedUp = false;

    public GameObject representedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        GetComponent<Outline3D>().SetOutlineActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            GetComponent<Outline3D>().SetOutlineActive(false);
    }

    private void OnTriggerStay (Collider other) {
        if (!pickedUp && Input.GetKeyDown(KeyCode.E) && other.GetComponent<PlayerShoot>() is PlayerShoot player && player.CanPickupGun()) {
            GameEvents.InvokePickupItem(player.gameObject, GameObject.Instantiate(representedObject));
            pickedUp = true;
            GameObject.Destroy(gameObject);
        }
    }
}
