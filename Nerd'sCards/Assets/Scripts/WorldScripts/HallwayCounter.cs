using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayCounter : MonoBehaviour
{
    [SerializeField] private List<Collider> hallway = new List<Collider>();

    public void DeactivateWall()
    {
        foreach (Collider thisobject in hallway)
        {
            thisobject.isTrigger = false;
        }
    }

    public void ActivateWall()
    {
        foreach (Collider thisobject in hallway)
        {
            thisobject.isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Closed Exit")
        {
            hallway.Add(other.gameObject.GetComponent<BoxCollider>());
        }
    }

}
