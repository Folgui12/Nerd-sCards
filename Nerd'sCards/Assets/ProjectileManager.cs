using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private float lifeTime;

    private float currentLifeTime;
 
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 4f;
        currentLifeTime = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;

        currentLifeTime += Time.deltaTime;

        if(currentLifeTime >= lifeTime)
            Destroy(this.gameObject);
    }
}
