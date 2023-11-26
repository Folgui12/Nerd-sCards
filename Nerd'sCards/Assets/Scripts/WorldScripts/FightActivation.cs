using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightActivation : MonoBehaviour
{
    [SerializeField] private List<Behaviour> Enemys = new List<Behaviour>();
    public int amountOfEnemysInRoom = 0;

    [SerializeField] private UnityEvent startFight;
    [SerializeField] private UnityEvent stopFight;
     
    private void Start()
    {

    }

    void ActivateBattle()
    {
        foreach(Behaviour thisobject in Enemys)
        {
            amountOfEnemysInRoom++;
            thisobject.enabled = true;
        }
    }
    
    public void CountEnemys()
    {
        amountOfEnemysInRoom--;
        if(amountOfEnemysInRoom == 0)
        {
           stopFight.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("AskEnemy");
            Enemys.Add(other.GetComponent<EnemyAI>());
        }

        if (other.tag == "Player")
        {
            ActivateBattle();
            startFight.Invoke();
        }
    }
}
