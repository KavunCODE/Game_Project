using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Char_Teleport : MonoBehaviour
{
    private GameObject currentTeleport;
    private bool isTeleporting = false;

    // Update is called once per frame
    void Update()
    {
        if (currentTeleport != null && !isTeleporting)
        {
            TeleportTo(currentTeleport.GetComponent<TeleportScript>().GetDestination().position);
        }
    }

    private void TeleportTo(Vector3 targetPosition)
    {
        StartCoroutine(TeleportCooldown(targetPosition));
    }

    private IEnumerator TeleportCooldown(Vector3 targetPosition)
    {
        isTeleporting = true;

        // Perform your teleportation logic here
        transform.position = targetPosition;

        yield return new WaitForSeconds(2f); // Replace 2f with your desired delay

        isTeleporting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            currentTeleport = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport") && collision.gameObject == currentTeleport)
        {
            currentTeleport = null;
        }
    }
}
