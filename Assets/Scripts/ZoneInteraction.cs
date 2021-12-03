using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneInteraction : MonoBehaviour
{
    Color zoneColor;
    Color baseColor;
    private int counter = 0;
    private bool playerPresent = false;
    private bool zoneCaptured = false;
    private GameObject player;

    void Start()
    {
        baseColor = gameObject.GetComponent<Renderer>().material.color = Color.white;
        zoneColor = ColorManager.pickColor(Random.Range(0, ColorManager.colors.Length));
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (!zoneCaptured) {
            // Handle zone capture
            if (playerPresent) {
                // Gradually change zone's color
                gameObject.GetComponent<Renderer>().material.color = Color.Lerp(baseColor, zoneColor, (float) counter/100);
                counter++;
            } else {
                gameObject.GetComponent<Renderer>().material.color = baseColor;
                counter = 0;
            }
            if (counter == 100) {
                zoneCaptured = true;
            }
        } else {
            // Reaload ammo
            if (playerPresent && !player.GetComponent<PlayerFire>().isColorAmmoFull(zoneColor)) {
                int newColorAmmoAmount = player.GetComponent<PlayerFire>().reloadColorAmmo(zoneColor, 1);
            }

        }
    }

    private void  OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            playerPresent = false;
        }
    }
}
