using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private IsometricMovement Player;

    private Transform playerCurrentRoom;

    public Vector3 offset;
    public Camera cam;

    private void Start()
    {
        Player = FindObjectOfType<IsometricMovement>();
        playerCurrentRoom = Player.currentRoom;
        cam = GetComponent<Camera>();
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