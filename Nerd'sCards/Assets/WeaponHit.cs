using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    private PlayerAttackSystem attackSystem;

    // Start is called before the first frame update
    void Start()
    {
        attackSystem = GetComponentInParent<PlayerAttackSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(attackSystem.knockBackForce, attackSystem.currentDamage);
        }
    }
}
