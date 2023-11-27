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
        nav = GetComponentInParent<NavMeshAgent>();
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
    
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            PlayerAttackSystem playerWeapon = other.gameObject.GetComponentInParent<PlayerAttackSystem>();
            TakeDamage(playerWeapon.knockBackForce, playerWeapon.currentDamage);
        }           
    }*/

    public void TakeDamage(float knockbackForce, float damageRecevied)
    {
        transform.position -= transform.forward * Time.deltaTime * knockbackForce;
        life -= damageRecevied;

        if(life <= 0)
            fightActive.CountEnemys();
    }

    private void HitAnimation()
    {
        anim.SetTrigger("TakeDamage");
    }
}
