using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public int maxLife;
    public int currentLife;

    private IsometricMovement playerRef;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        playerRef = GetComponent<IsometricMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Enemy")
        {
            TakeDamage();
        }  
    }

    public void TakeDamage()
    {
        currentLife--;

        HealthManager.Instance.CheckLifeStat(currentLife);

        if (currentLife <= 0)
            ScreenManager.Instance.PlayerLose();

    }
}
