using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    private PlayerAttackSystem attackSystem;
    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        attackSystem = GetComponentInParent<PlayerAttackSystem>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            DeactiveCollider(); 
            other.GetComponent<EnemyAI>().TakeDamage(attackSystem.knockBackForce, attackSystem.currentDamage);
        }
    }

    public void ActiveCollider() => col.enabled = true;

    public void DeactiveCollider() => col.enabled = false;
}
