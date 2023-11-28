using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackSystem : MonoBehaviour
{
    public Animator anim;
    public bool isAttacking = false;
    public List<PlayerAttackSO> combo;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;
    public float currentDamage;
    public float knockBackForce;

    private IsometricMovement player;
    private WeaponHit ruler;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponentInParent<IsometricMovement>();
        knockBackForce = 40f;
        ruler = GetComponentInChildren<WeaponHit>();
    }

    // Update is called once per frame
    private void Update()
    {
        ExitAttack();
    }

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        ruler.ActiveCollider();
        isAttacking = true;
        if (context.performed)
            PlayerAttackSO();
    }

    private void PlayerAttackSO()
    {
        if(Time.time - lastComboEnd > 1f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= .8f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                currentDamage = combo[comboCounter].damage;
                lastClickedTime = Time.time;
                comboCounter++;

                if(comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
                    
            }

        }

    }

    private void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9 && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            Invoke("EndCombo", .5f);
    }

    private void EndCombo()
    {
        ruler.DeactiveCollider();
        isAttacking = false;
        comboCounter = 0;
        lastComboEnd = Time.time;
        currentDamage = 0; 
    }
}
