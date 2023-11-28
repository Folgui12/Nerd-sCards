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
    private Animations anim;

    [SerializeField] float meleeAttackDistance;
    [SerializeField] float AttackCooldown;
    private float AttackTimer;

    public bool Active;

    void Start()
    {
        anim = GetComponentInParent<Animations>();
        nav = GetComponentInParent<NavMeshAgent>();

        Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(playerTransform.position);
        anim.MoveAnim();

        if (anim.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
            nav.isStopped = false;

        AttackTimer += Time.deltaTime;


        canAttack();
    }

    private void canAttack()
    {
        float Distance;
        Distance = Vector3.Distance(playerTransform.position, transform.position);
        if (Distance < meleeAttackDistance && AttackCooldown <= AttackTimer)
        {
            AttackTimer = 0;
            anim.Attack();
        }
    }
    
    public void TakeDamage(float knockbackForce, float damageRecevied)
    {
        nav.isStopped = true;
        transform.position -= transform.forward * Time.deltaTime * knockbackForce;
        life -= damageRecevied;

        anim.HitAnimation();

        if(life <= 0)
        {
            PlayerAmmoManager.Instance.currentAmmo++;
            PlayerAmmoManager.Instance.CheckAmmoStat();
            fightActive.CountEnemys();
            Destroy(this.gameObject);
        }
    }
}
