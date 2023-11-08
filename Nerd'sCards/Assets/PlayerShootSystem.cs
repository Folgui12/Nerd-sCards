using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private float maxFloatTime;
    
    [SerializeField] private GameObject projectile;
    private float holdTime;

    private Transform weapon;
    private IsometricMovement characterMovRef;

    private bool startCount;

    // Start is called before the first frame update
    void Start()
    {
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

    private void ReStartHoldTime()
    {
        holdTime = 0;
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

            GameObject bulletTransform = Instantiate(projectile, weapon.position, Quaternion.identity);
        
            Vector3 projectileDir = (characterMovRef.playerLookAt - weapon.position).normalized;

            bulletTransform.GetComponent<ProjectileManager>().SetUpBullet(projectileDir, holdTime);

            ReStartHoldTime();
        }
    }
}
