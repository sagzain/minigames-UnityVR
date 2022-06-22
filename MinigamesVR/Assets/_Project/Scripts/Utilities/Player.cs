using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private string playerName;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    public string GetPlayerName() => playerName;

    public void MovePlayerTo(Vector3 position)
    {
        _characterController.Move(position);
        // transform.position = position;
    }
}