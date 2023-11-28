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

    [SerializeField] private Animator anim;

    
    public Transform weapon;
    public IsometricMovement characterMovRef;
    private Vector3 projectileDir;

    // Holding Shot
    private bool startCount; 
    private float holdTime;

    // Start is called before the first frame update
    void Start()
    {
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

        projectileDir = (weapon.position - transform.position).normalized;
    }

    private void ReStartHoldTime()
    {
        holdTime = 0;
    }

    public void RangeAttack(InputAction.CallbackContext context)
    {
        if (characterMovRef.canRange)
        {
            if(context.started && PlayerAmmoManager.Instance.currentAmmo > 0)
            {
                startCount = true;
                characterMovRef.walkWhileAiming();
            }
            if (context.canceled && PlayerAmmoManager.Instance.currentAmmo > 0)
            {

                anim.Play("ThrowRock", 0, 0);

                PlayerAmmoManager.Instance.currentAmmo--;

                PlayerAmmoManager.Instance.CheckAmmoStat();

                startCount = false;

                characterMovRef.walkWhioutAiming();

                GameObject bulletTransform = Instantiate(projectile, weapon.position, Quaternion.identity);

                bulletTransform.GetComponent<ProjectileManager>().SetUpBullet(new Vector3(projectileDir.x, 0, projectileDir.z), holdTime);

                ReStartHoldTime();
            }
        }
    }
}
