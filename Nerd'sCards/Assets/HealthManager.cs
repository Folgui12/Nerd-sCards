using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    [SerializeField] private PlayerLifeManager playerRef;
    [SerializeField] private Image[] lifePoints;
    [SerializeField] private Sprite lifePointFull;
    [SerializeField] private Sprite lifePointEmpty;
    private int health;
    private int maxHealth;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = playerRef.maxLife;
        health = playerRef.currentLife;
        CheckLifeStat(maxHealth);
    }

    public void CheckLifeStat(int currentLife)
    {
        health = currentLife;

        for (int i = 0; i < lifePoints.Length; i++)
        {
            if(i < health)
                lifePoints[i].sprite = lifePointFull;
            else
                lifePoints[i].sprite = lifePointEmpty;

            if (i < maxHealth)
                lifePoints[i].enabled = true;
            else
                lifePoints[i].enabled = false;
        }
    }
}
