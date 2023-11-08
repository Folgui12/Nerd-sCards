using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothFactor;

    private IsometricMovement Player;

    private Transform playerCurrentRoom;

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
        transform.position = Player.currentRoom.position + new Vector3(-12f, 11.5f, -12);
    }
}