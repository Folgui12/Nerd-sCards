using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{

    [SerializeField] private float speed;
    public Vector3 Bulletdir;

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    public void Travel() => transform.position +=  Bulletdir * Time.deltaTime * speed;
}
