using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void MoveAnim()
    {
        anim.SetBool("IsRunning", true);
    }

    public void Attack()
    {
        anim.SetTrigger("IsAttacking");
    }

    public void HitAnimation()
    {
        anim.SetTrigger("TakeDamage");
    }
}
