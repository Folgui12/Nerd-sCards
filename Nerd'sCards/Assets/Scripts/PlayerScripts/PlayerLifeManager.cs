using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private float inmuTimer;
    public int maxLife;
    public int currentLife;

    private bool canTakeDamage;
    private float inmuTime;

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
        currentLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canTakeDamage && inmuTime < inmuTimer)
            inmuTime += Time.deltaTime;
        
        if(inmuTime > inmuTimer)
        {
            canTakeDamage = true;
            inmuTime = 0;
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Enemy" && canTakeDamage)
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        canTakeDamage = false;

        currentLife--;

        HealthManager.Instance.CheckLifeStat(currentLife);

        if (currentLife <= 0)
            ScreenManager.Instance.PlayerLose();

    }
}
