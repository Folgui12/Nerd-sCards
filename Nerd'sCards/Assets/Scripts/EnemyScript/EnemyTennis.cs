using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTennis : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] float rangedAttackDistance;
    [SerializeField] Transform shoot;

    [SerializeField] GameObject Bullet;
    [SerializeField] float TimeBetweenShots;
    private float attackTimer;

    private EnemyAI AI;
    private Animations anim;

    private void Start()
    {
        anim = GetComponentInParent<Animations>();
        AI = GetComponent<EnemyAI>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (AI.Active)
        {
            RangedAttack();
        }
    }

    public void RangedAttack()
    {
        float Distance;
        Distance = Vector3.Distance(playerTransform.position, transform.position);
        if (Distance < rangedAttackDistance && TimeBetweenShots <= attackTimer)
        {
            anim.Attack();          
            GameObject _bullet = Instantiate(Bullet, shoot.transform.position, shoot.transform.rotation);
            TennisBall tennis = _bullet.GetComponent<TennisBall>();
            tennis.Bulletdir = playerTransform.position - shoot.transform.position;
            attackTimer = 0;
        }
    }
}
