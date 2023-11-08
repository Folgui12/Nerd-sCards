using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private float maxFloatTime;
    
    [SerializeField] private GameObject projectile;
    private PlayerAttackSystem pjAS;
    private float holdTime;

    private Transform weapon;
    private IsometricMovement characterMovRef;

    private bool startCount;

    // Start is called before the first frame update
    void Start()
    {
        pjAS = GameObject.Find("RightHand").GetComponent<PlayerAttackSystem>();
        weapon = GetComponentInChildren<Transform>();
        characterMovRef = GetComponentInParent<IsometricMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startCount)
        {
            holdTime += Time.deltaTime;
        }
    }

    public void RangeAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            startCount = true;
        }

        if (context.canceled)
        {
            startCount = false;
            Instantiate(projectile, weapon.position, Quaternion.LookRotation(characterMovRef.playerLookAt.normalized, Vector3.up));
        }
    }
}
