using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atmosphere : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has a tag of "InnerObject"
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Attractor>().enabled = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Attractor>().enabled = false;
        }
    }
}
