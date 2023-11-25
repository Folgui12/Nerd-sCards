using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool IsDeath;
    public FightActivation fightActive;
    public float life;
    public NavMeshAgent nav;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(playerTransform.position);
    
        if(life <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //fightActive.CountEnemys();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            PlayerAttackSystem playerWeapon = other.gameObject.GetComponentInParent<PlayerAttackSystem>();
            TakeDamage(playerWeapon.knockBackForce, playerWeapon.currentDamage);
        }           
    }

    public void TakeDamage(float knockbackForce, float damageRecevied)
    {
        fightActive.CountEnemys();
        anim.SetTrigger("TakeDamage");
        transform.position -= transform.forward * Time.deltaTime * knockbackForce;
        life -= damageRecevied;
    }
}
