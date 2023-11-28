using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmoManager : MonoBehaviour
{
    public static PlayerAmmoManager Instance;

    [SerializeField] private Image[] ammoPoints;
    [SerializeField] private Sprite lifePointFull;
    public float maxAmmo;
    public float currentAmmo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        CheckAmmoStat();
    }

    public void CheckAmmoStat()
    {
        for (int i = 0; i < ammoPoints.Length; i++)
        {
            if (i < currentAmmo)
                ammoPoints[i].color = Color.white;
            else
                ammoPoints[i].color = Color.clear;

            if (i < maxAmmo)
                ammoPoints[i].enabled = true;
            else
                ammoPoints[i].enabled = false;
        }
    }
}
