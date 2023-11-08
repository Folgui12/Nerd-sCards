using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float knockBackForce;

    private float lifeTime;
    private Vector3 newDir;
    private float currentLifeTime;
 
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 2f;
        currentLifeTime = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += newDir * speed * Time.deltaTime;

        currentLifeTime += Time.deltaTime;

        if(currentLifeTime >= lifeTime)
            Destroy(this.gameObject);
    }

    public void SetUpBullet(Vector3 dir, float holdTime)
    {
        newDir = dir;

        if(holdTime < 1)
        {
            damage = 2;
            knockBackForce = 10;
        }

        else if(holdTime > 1 && holdTime < 2)
        {
            damage = 3;
            transform.localScale *= 1.5f;
            knockBackForce = 20;
        }

        else if(holdTime > 2 && holdTime < 3)
        {
            damage = 4;
            transform.localScale *= 2f;
            knockBackForce = 30;
        }

        else
        {
            damage = 5;
            transform.localScale *= 3f;
            knockBackForce = 40;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAI>().TakeDamage(knockBackForce, damage);
            Destroy(gameObject);
        }
    }
}
