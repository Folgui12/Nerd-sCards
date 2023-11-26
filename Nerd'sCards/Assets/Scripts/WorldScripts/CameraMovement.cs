using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private IsometricMovement Player;

    private Transform playerCurrentRoom;

    [SerializeField] private Vector3 offset;

    private void Start()
    {
        Player = FindObjectOfType<IsometricMovement>();
        playerCurrentRoom = Player.currentRoom;
    }

    private void Update()
    {
        if(Player.currentRoom != playerCurrentRoom)
        {
            playerCurrentRoom = Player.currentRoom;
            followPlayer();
        }
    }

    private void followPlayer()
    {
        transform.position = Player.currentRoom.position + offset;
    }
}