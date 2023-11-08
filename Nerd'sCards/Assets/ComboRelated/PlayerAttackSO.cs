using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/MeleeAttack")]

public class PlayerAttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damage;
}
