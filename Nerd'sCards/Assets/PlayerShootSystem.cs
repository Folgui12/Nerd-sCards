using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShootSystem : MonoBehaviour
{
    [SerializeField] private float maxHoldingTime;
    
    [SerializeField] private GameObject projectile;

    [SerializeField] private Image cooldownImage;

    private float holdTime;
    private Transform weapon;
    private IsometricMovement characterMovRef;

    private bool startCount;

    private Vector3 projectileDir;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Transform>();
        characterMovRef = GetComponentInParent<IsometricMovement>();
        cooldownImage.fillAmount = 0;
        cooldownImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startCount)
        {
            cooldownImage.gameObject.SetActive(true);

            holdTime += Time.deltaTime;

            cooldownImage.fillAmount = holdTime / (maxHoldingTime + 1);
        }

        else
        {
            cooldownImage.fillAmount = 0;
            cooldownImage.gameObject.SetActive(false);
        }

        projectileDir = (characterMovRef.playerLookAt - weapon.position).normalized;
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
            characterMovRef.walkWhileAiming();
        }

        if (context.canceled)
        {
            startCount = false;

            characterMovRef.walkWhioutAiming();

            GameObject bulletTransform = Instantiate(projectile, weapon.position, Quaternion.identity);

            bulletTransform.GetComponent<ProjectileManager>().SetUpBullet(projectileDir, holdTime);

            ReStartHoldTime();
        }
    }
}
