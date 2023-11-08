using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackSystem : MonoBehaviour
{
    private Animator anim;
    public bool canAttack = true;
    public List<PlayerAttackSO> combo;
    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;
    public float currentDamage;
    public float knockBackForce;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        knockBackForce = 40f;
    }

    // Update is called once per frame
    private void Update()
    {
        ExitAttack();
    }

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            PlayerAttackSO();
    }

    private void PlayerAttackSO()
    {
        if(Time.time - lastComboEnd > .5f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= .3f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                currentDamage = combo[comboCounter].damage;
                lastClickedTime = Time.time;
                comboCounter++;

                if(comboCounter >= combo.Count)
                {
                    EndCombo();
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
        comboCounter = 0;
        lastComboEnd = Time.time;
        currentDamage = 0; 
    }
}
