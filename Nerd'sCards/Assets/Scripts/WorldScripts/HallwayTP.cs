using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayTP : MonoBehaviour
{
    [SerializeField] private Transform TPtoo;
    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.transform.position = TPtoo.transform.position;
            Debug.Log("TP");
        }
    }
}
